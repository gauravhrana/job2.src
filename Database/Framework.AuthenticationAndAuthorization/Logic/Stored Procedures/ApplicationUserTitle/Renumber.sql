IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleReNumber')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleReNumber'
	DROP   Procedure ApplicationUserTitleReNumber
END
GO

PRINT 'Creating Procedure ApplicationUserTitleReNumber'
GO

/******************************************************************************

**		File: 
**		Name: ApplicationUserTitleReNumber
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC ApplicationUserTitleReOrder	 NULL	, NULL	, NULL
			EXEC ApplicationUserTitleReOrder	 NULL	, 'K'	, NULL
			EXEC ApplicationUserTitleReOrder	 1		, 'K'	, NULL
			EXEC ApplicationUserTitleReOrder   1		, NULL	, NULL
			EXEC ApplicationUserTitleReOrder   NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
** 
******************************************************************************/
Create Procedure dbo.ApplicationUserTitleReNumber
(
		@ApplicationUserTitleId				INT				= NULL	
	,	@Direction				INT				= NULL	
	,	@Seed					INT				= NULL	
	,	@Increment				INT				= NULL	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityTypeId		INT				= 7100
)
AS
BEGIN
DECLARE @RenumberingStatus AS VARCHAR(50)
SELECT @RenumberingStatus = IsUnderRenumbering from
CommonServices.dbo.EntityRenumberStatus WHERE EntityName='ApplicationUserTitle'

IF @RenumberingStatus = 'NO'
PRINT 'IN LOOP'
BEGIN
--Drop Unique constraint if exists
IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ApplicationUserTitle]')
	AND		name	= N'UQ_ApplicationUserTitle_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationUserTitle_Name_ApplicationId'
	ALTER	TABLE dbo.ApplicationUserTitle
	DROP	CONSTRAINT	UQ_ApplicationUserTitle_Name_ApplicationId
END

--Update renumbering status in EntityRenumberstatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='YES' WHERE EntityName='ApplicationUserTitle'

--Create table to track IDS
CREATE TABLE #IDTrack
(
			_OldID			INT					
		,	_NewID			INT					
		,	_HighNumberID   INT

)
-- Count number oif rows in ApplicationUserTitle
		DECLARE @Count AS INT = 0
		SELECT @Count = COUNT(*) FROM dbo.ApplicationUserTitle


		--Initialize loop iterator
		DECLARE @iCNT AS INT
		SET @iCNT = 1

		--declare and initialize high ID value
		DECLARE @highIDValue AS INT
		SET @highIDValue = 1000000

		--temp variable for old ApplicationUserTitleid
		DECLARE @tmpApplicationUserTitleId AS INT

		Select * into #tmpApplicationUserTitle from dbo.ApplicationUserTitle

		--this loop iterates and inserts high valued existing records into dbo.ApplicationUserTitle
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
	    DECLARE @SortOrder AS INT
	    SELECT  @SortOrder = MIN(SortOrder) from #tmpApplicationUserTitle 
		--Select single record from ApplicationUserTitle table
		SELECT @tmpApplicationUserTitleId  = ApplicationUserTitleId FROM #tmpApplicationUserTitle a WHERE SortOrder = @SortOrder
		
		
		--Make a copy of record with highidvalue
		IF @tmpApplicationUserTitleId != @highIDValue AND @tmpApplicationUserTitleId > 0
		BEGIN
		INSERT INTO dbo.ApplicationUserTitle (ApplicationUserTitleId, Name, Description, SortOrder, ApplicationId)
		SELECT @highIDValue,  Name, Description, SortOrder, ApplicationId
		FROM dbo.ApplicationUserTitle		
		WHERE ApplicationUserTitleId = @tmpApplicationUserTitleId   
		
		--make an entry in IDTrack table
		--build IDTrack table
		INSERT INTO #IDTrack
		Select @tmpApplicationUserTitleId AS _OldID, @Seed AS _NewID, @highIDValue AS _HighNumberID
		
		INSERT INTO CommonServices.dbo.EntityIDTrack (EntityId, OldId, NewEntityId, HighNumberId, EntityName)
		VALUES( @SystemEntityTypeId, @tmpApplicationUserTitleId, @Seed, @highIDValue, 'ApplicationUserTitle')
	    
	    
		--Increment Seed, loop counter and highidvalue
		SET @Seed = @Seed + @Increment
		SET @highIDValue = @highIDValue + 1
		
		END
		SET @iCNT = @iCNT + 1
		--delete processed record from temp table of ApplicationUserTitle
		DELETE FROM #tmpApplicationUserTitle WHERE ApplicationUserTitleId = @tmpApplicationUserTitleId

		SET ROWCOUNT 0

		END 

		DROP TABLE #tmpApplicationUserTitle
		
		Select * INTO #tmp1IDTrack from #IDTrack

		Select @Count = COUNT(*) from #tmp1IDTrack
		SET @iCNT = 0
		--This loop updates FK Tables to refer to high ID records of ApplicationUserTitle
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @HighNumberId AS INT
		SELECT @HighNumberId = MAX(_HighNumberiD)
		FROM #tmp1IDTrack
		--select old id from temp table of idtrack
		SELECT @tmpApplicationUserTitleId =  _OldID FROM #tmp1IDTrack WHERE _HighNumberId = @HighNumberId
		
		--Update Project table with highnumber ApplicationUserTitleid in the place of old ApplicationUserTitleid
		UPDATE dbo.ApplicationUser
		SET ApplicationUserTitleId = @HighNumberId	
		WHERE ApplicationUserTitleId = @tmpApplicationUserTitleId   
		
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from tamp table of idtrack
		DELETE FROM #tmp1IDTrack WHERE _OldID = @tmpApplicationUserTitleId AND _HighNumberID=@HighNumberId

		SET ROWCOUNT 0

		END
		
		
		--******************
		
		--copy records of IDTrack into temp table
		Select * INTO #tmp0IDTrack from #IDTrack
		SET @iCNT = 0

		--Declare temp variables for New and High ApplicationUserTitle Ids
		DECLARE @tmpNewApplicationUserTitleId AS INT
		DECLARE @tmpHighApplicationUserTitleId AS INT

		--Count the number of rows in ApplicationUserTitle table
		select @Count=Count(*) from dbo.ApplicationUserTitle
		
		--this loop deletes old records from dbo.ApplicationUserTitle
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from temp IDTrack table
		SELECT @tmpNewApplicationUserTitleId = MAX(_NewID)
		FROM #tmp0IDTrack
		
		--Get old and High Number Ids for the selected New Id from IDTrack table
		SELECT @tmpApplicationUserTitleId =  _OldID, @tmpHighApplicationUserTitleId = _HighNumberID  FROM #tmp0IDTrack WHERE _NewID = @tmpNewApplicationUserTitleId
		
		--Delete old record from the original table in DB
		IF @tmpApplicationUserTitleId != @tmpNewApplicationUserTitleId AND @tmpApplicationUserTitleId > 0
		BEGIN
		DELETE FROM dbo.ApplicationUserTitle WHERE ApplicationUserTitleId = @tmpApplicationUserTitleId
		END

		-- Increment Loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp IDTrack table
		DELETE FROM #tmp0IDTrack WHERE _OldID = @tmpApplicationUserTitleId AND _NewID=@tmpNewApplicationUserTitleId

		SET ROWCOUNT 0
		END

		
		--make a copy of IDTrack table to iterate in loop
		Select * INTO #tmpIDTrack from #IDTrack
		
		--Set the count of number of rows in ApplicationUserTitle table
		select @Count=Count(*) from dbo.ApplicationUserTitle

		SET @iCNT = 1
		--This loop inserts existing records with new id values
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--select new ApplicationUserTitle id from idtrack table
		SELECT @tmpNewApplicationUserTitleId = MAX(_NewID)
		FROM #tmpIDTrack

		--select oldid from idtrack table
		SELECT @tmpApplicationUserTitleId =  _HighNumberID FROM #tmpIDTrack WHERE _NewID = @tmpNewApplicationUserTitleId
		--make a copy of existing record with new ID VALUE
		IF @tmpNewApplicationUserTitleId IS NOT NULL AND @tmpApplicationUserTitleId != @tmpNewApplicationUserTitleId AND NOT EXISTS (Select * from dbo.ApplicationUserTitle WHERE ApplicationUserTitleId = @tmpNewApplicationUserTitleID)
		BEGIN
		INSERT INTO dbo.ApplicationUserTitle (ApplicationUserTitleId, Name, Description, SortOrder, ApplicationId)
		SELECT @tmpNewApplicationUserTitleId,  Name, Description, SortOrder, ApplicationId
		FROM dbo.ApplicationUserTitle		
		WHERE ApplicationUserTitleId = @tmpApplicationUserTitleId    
		END   
		
		SET @iCNT = @iCNT + 1
		
		--DELETE processed id record from temp tab;le
		DELETE FROM #tmpIDTrack WHERE _HighNumberID = @tmpApplicationUserTitleId AND _NewID=@tmpNewApplicationUserTitleId

		SET ROWCOUNT 0

		END

		
		DROP TABLE #tmpIDTrack

		--Copy IDTrack contents into temp table
		Select * INTO #tmp2IDTrack from #IDTrack

		--Set counter to number of rows in temp table of IDTrack
		Select @Count = COUNT(*) from #tmp2IDTrack
		SET @iCNT = 1
		--This loop updates FK Tables to refer to new ID records of ApplicationUserTitle
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @tmpHighId AS INT
		SELECT @tmpHighId = MAX(_HighNumberId)
		FROM #tmp2IDTrack

		--select new id from temp table of idtrack
		SELECT @tmpNewApplicationUserTitleId =  _NewID FROM #tmp2IDTrack WHERE _HighNumberID = @tmpHighId
		
		--Update Project table with new ApplicationUserTitleid in the place of highnumber ApplicationUserTitleid
		UPDATE dbo.ApplicationUser
		SET ApplicationUserTitleId = @tmpNewApplicationUserTitleId	
		WHERE ApplicationUserTitleId = @tmpHighId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from temp table of idtrack
		DELETE FROM #tmp2IDTrack WHERE _NewID = @tmpNewApplicationUserTitleId AND _HighNumberID=@tmpHighId

		SET ROWCOUNT 0

		END

		Select * INTO #tmp3IDTrack from #IDTrack
		SET @iCNT = 1
		
		--Set counter to number of rows in ApplicationUserTitle table
		select @Count=Count(*) from dbo.ApplicationUserTitle
		
		--Select * from dbo.ApplicationUserTitle
		--this loop deleteshighnumbered records from dbo.ApplicationUserTitle
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from IDTrack table
		SELECT @tmpNewApplicationUserTitleId = MAX(_NewID)
		FROM #tmp3IDTrack

		--Get old and high number Ids for the selected New Id value
		SELECT @tmpApplicationUserTitleId =  _OldID, @tmpHighApplicationUserTitleId = _HighNumberID  FROM #tmp3IDTrack WHERE _NewID = @tmpNewApplicationUserTitleId
		
		--Delete High numbered Record from original table
		DELETE FROM dbo.ApplicationUserTitle WHERE ApplicationUserTitleId = @tmpHighApplicationUserTitleId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp table of IDTrack
		DELETE FROM #tmp3IDTrack WHERE _OldID = @tmpApplicationUserTitleId AND _NewID=@tmpNewApplicationUserTitleId

		SET ROWCOUNT 0
		END

		--Update status in EntityRenumberStatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='NO' WHERE EntityName='ApplicationUserTitle'

		--Create back the Constraint once renumbering is done
		PRINT 'Creating Constraint'
		ALTER TABLE dbo.ApplicationUserTitle
		ADD CONSTRAINT UQ_ApplicationUserTitle_Name_ApplicationId UNIQUE NONCLUSTERED
		(
				ApplicationId
			,	Name	
		)

		END
		END
		GO


