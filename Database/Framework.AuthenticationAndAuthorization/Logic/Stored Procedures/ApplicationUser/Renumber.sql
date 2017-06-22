IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserReNumber')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserReNumber'
	DROP   Procedure ApplicationUserReNumber
END
GO

PRINT 'Creating Procedure ApplicationUserReNumber'
GO

/******************************************************************************

**		File: 
**		Name: ApplicationUserReNumber
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
			EXEC ApplicationUserReOrder	 NULL	, NULL	, NULL
			EXEC ApplicationUserReOrder	 NULL	, 'K'	, NULL
			EXEC ApplicationUserReOrder	 1		, 'K'	, NULL
			EXEC ApplicationUserReOrder   1		, NULL	, NULL
			EXEC ApplicationUserReOrder   NULL	, NULL	, 'W'

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
Create Procedure dbo.ApplicationUserReNumber
(
		@ApplicationUserId				INT				= NULL	
	,	@Direction				INT				= NULL	
	,	@Seed					INT				= NULL	
	,	@Increment				INT				= NULL	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityTypeId		INT				= 6900
)
AS
BEGIN
DECLARE @RenumberingStatus AS VARCHAR(50)
SELECT @RenumberingStatus = IsUnderRenumbering from
CommonServices.dbo.EntityRenumberStatus WHERE EntityName='ApplicationUser'

IF @RenumberingStatus = 'NO'
PRINT 'IN LOOP'
BEGIN
--Drop Unique constraint if exists
IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ApplicationUser]')
	AND		name	= N'UQ_ApplicationUser_LastName_FirstName'
)
BEGIN
	PRINT 'Dropping UQ_ApplicationUser_LastName'
	ALTER TABLE dbo.ApplicationUser
	DROP CONSTRAINT UQ_ApplicationUser_LastName_FirstName
END

--Update renumbering status in EntityRenumberstatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='YES' WHERE EntityName='ApplicationUser'

--Create table to track IDS
CREATE TABLE #IDTrack
(
			_OldID			INT					
		,	_NewID			INT					
		,	_HighNumberID   INT

)
-- Count number oif rows in ApplicationUser
		DECLARE @Count AS INT = 0
		SELECT @Count = COUNT(*) FROM dbo.ApplicationUser


		--Initialize loop iterator
		DECLARE @iCNT AS INT
		SET @iCNT = 1

		--declare and initialize high ID value
		DECLARE @highIDValue AS INT
		SET @highIDValue = 1000000

		--temp variable for old ApplicationUserid
		DECLARE @tmpApplicationUserId AS INT

		Select * into #tmpApplicationUser from dbo.ApplicationUser

		--this loop iterates and inserts high valued existing records into dbo.ApplicationUser
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
	    DECLARE @SortOrder AS INT
	    SELECT  @SortOrder = MIN(SortOrder) from #tmpApplicationUser 
		--Select single record from ApplicationUser table
		SELECT @tmpApplicationUserId  = ApplicationUserId FROM #tmpApplicationUser a WHERE SortOrder = @SortOrder
		
		
		--Make a copy of record with highidvalue
		IF @tmpApplicationUserId != @highIDValue AND @tmpApplicationUserId > 0
		BEGIN
		INSERT INTO dbo.ApplicationUser (ApplicationUserId, FirstName, LastName, MiddleName, ApplicationId, ApplicationUserTitleId, ApplicationUserName)
		SELECT @highIDValue, FirstName, LastName, MiddleName, ApplicationId, ApplicationUserTitleId, ApplicationUserName
		FROM dbo.ApplicationUser		
		WHERE ApplicationUserId = @tmpApplicationUserId   
		
		--make an entry in IDTrack table
		--build IDTrack table
		INSERT INTO #IDTrack
		Select @tmpApplicationUserId AS _OldID, @Seed AS _NewID, @highIDValue AS _HighNumberID
		
		INSERT INTO CommonServices.dbo.EntityIDTrack (EntityId, OldId, NewEntityId, HighNumberId, EntityName)
		VALUES( @SystemEntityTypeId, @tmpApplicationUserId, @Seed, @highIDValue, 'ApplicationUser')
	    
	    
		--Increment Seed, loop counter and highidvalue
		SET @Seed = @Seed + @Increment
		SET @highIDValue = @highIDValue + 1
		
		END
		SET @iCNT = @iCNT + 1
		--delete processed record from temp table of ApplicationUser
		DELETE FROM #tmpApplicationUser WHERE ApplicationUserId = @tmpApplicationUserId

		SET ROWCOUNT 0

		END 

		DROP TABLE #tmpApplicationUser
		
		Select * INTO #tmp1IDTrack from #IDTrack

		Select @Count = COUNT(*) from #tmp1IDTrack
		SET @iCNT = 0
		--This loop updates FK Tables to refer to high ID records of ApplicationUser
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @HighNumberId AS INT
		SELECT @HighNumberId = MAX(_HighNumberiD)
		FROM #tmp1IDTrack
		--select old id from temp table of idtrack
		SELECT @tmpApplicationUserId =  _OldID FROM #tmp1IDTrack WHERE _HighNumberId = @HighNumberId
		
		--Update Project table with highnumber ApplicationUserid in the place of old ApplicationUserid
		UPDATE dbo.ApplicationUserXApplicationRole
		SET ApplicationUserId = @HighNumberId	
		WHERE ApplicationUserId = @tmpApplicationUserId   
		
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from tamp table of idtrack
		DELETE FROM #tmp1IDTrack WHERE _OldID = @tmpApplicationUserId AND _HighNumberID=@HighNumberId

		SET ROWCOUNT 0

		END
		
		
		--******************
		
		--copy records of IDTrack into temp table
		Select * INTO #tmp0IDTrack from #IDTrack
		SET @iCNT = 0

		--Declare temp variables for New and High ApplicationUser Ids
		DECLARE @tmpNewApplicationUserId AS INT
		DECLARE @tmpHighApplicationUserId AS INT

		--Count the number of rows in ApplicationUser table
		select @Count=Count(*) from dbo.ApplicationUser
		
		--this loop deletes old records from dbo.ApplicationUser
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from temp IDTrack table
		SELECT @tmpNewApplicationUserId = MAX(_NewID)
		FROM #tmp0IDTrack
		
		--Get old and High Number Ids for the selected New Id from IDTrack table
		SELECT @tmpApplicationUserId =  _OldID, @tmpHighApplicationUserId = _HighNumberID  FROM #tmp0IDTrack WHERE _NewID = @tmpNewApplicationUserId
		
		--Delete old record from the original table in DB
		IF @tmpApplicationUserId != @tmpNewApplicationUserId AND @tmpApplicationUserId > 0
		BEGIN
		DELETE FROM dbo.ApplicationUser WHERE ApplicationUserId = @tmpApplicationUserId
		END

		-- Increment Loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp IDTrack table
		DELETE FROM #tmp0IDTrack WHERE _OldID = @tmpApplicationUserId AND _NewID=@tmpNewApplicationUserId

		SET ROWCOUNT 0
		END

		
		--make a copy of IDTrack table to iterate in loop
		Select * INTO #tmpIDTrack from #IDTrack
		
		--Set the count of number of rows in ApplicationUser table
		select @Count=Count(*) from dbo.ApplicationUser

		SET @iCNT = 1
		--This loop inserts existing records with new id values
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--select new ApplicationUser id from idtrack table
		SELECT @tmpNewApplicationUserId = MAX(_NewID)
		FROM #tmpIDTrack

		--select oldid from idtrack table
		SELECT @tmpApplicationUserId =  _HighNumberID FROM #tmpIDTrack WHERE _NewID = @tmpNewApplicationUserId
		--make a copy of existing record with new ID VALUE
		IF @tmpNewApplicationUserId IS NOT NULL AND @tmpApplicationUserId != @tmpNewApplicationUserId AND NOT EXISTS (Select * from dbo.ApplicationUser WHERE ApplicationUserId = @tmpNewApplicationUserID)
		BEGIN
		INSERT INTO dbo.ApplicationUser (ApplicationUserId, FirstName, LastName, MiddleName, ApplicationId, ApplicationUserTitleId, ApplicationUserName)
		SELECT @tmpNewApplicationUserId, FirstName, LastName, MiddleName, ApplicationId, ApplicationUserTitleId, ApplicationUserName
		FROM dbo.ApplicationUser		
		WHERE ApplicationUserId = @tmpApplicationUserId    
		END   
		
		SET @iCNT = @iCNT + 1
		
		--DELETE processed id record from temp tab;le
		DELETE FROM #tmpIDTrack WHERE _HighNumberID = @tmpApplicationUserId AND _NewID=@tmpNewApplicationUserId

		SET ROWCOUNT 0

		END

		
		DROP TABLE #tmpIDTrack

		--Copy IDTrack contents into temp table
		Select * INTO #tmp2IDTrack from #IDTrack

		--Set counter to number of rows in temp table of IDTrack
		Select @Count = COUNT(*) from #tmp2IDTrack
		SET @iCNT = 1
		--This loop updates FK Tables to refer to new ID records of ApplicationUser
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @tmpHighId AS INT
		SELECT @tmpHighId = MAX(_HighNumberId)
		FROM #tmp2IDTrack

		--select new id from temp table of idtrack
		SELECT @tmpNewApplicationUserId =  _NewID FROM #tmp2IDTrack WHERE _HighNumberID = @tmpHighId
		
		--Update Project table with new ApplicationUserid in the place of highnumber ApplicationUserid
		UPDATE dbo.ApplicationUserXApplicationRole
		SET ApplicationUserId = @tmpNewApplicationUserId	
		WHERE ApplicationUserId = @tmpHighId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from temp table of idtrack
		DELETE FROM #tmp2IDTrack WHERE _NewID = @tmpNewApplicationUserId AND _HighNumberID=@tmpHighId

		SET ROWCOUNT 0

		END

		Select * INTO #tmp3IDTrack from #IDTrack
		SET @iCNT = 1
		
		--Set counter to number of rows in ApplicationUser table
		select @Count=Count(*) from dbo.ApplicationUser
		
		--Select * from dbo.ApplicationUser
		--this loop deleteshighnumbered records from dbo.ApplicationUser
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from IDTrack table
		SELECT @tmpNewApplicationUserId = MAX(_NewID)
		FROM #tmp3IDTrack

		--Get old and high number Ids for the selected New Id value
		SELECT @tmpApplicationUserId =  _OldID, @tmpHighApplicationUserId = _HighNumberID  FROM #tmp3IDTrack WHERE _NewID = @tmpNewApplicationUserId
		
		--Delete High numbered Record from original table
		DELETE FROM dbo.ApplicationUser WHERE ApplicationUserId = @tmpHighApplicationUserId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp table of IDTrack
		DELETE FROM #tmp3IDTrack WHERE _OldID = @tmpApplicationUserId AND _NewID=@tmpNewApplicationUserId

		SET ROWCOUNT 0
		END

		--Update status in EntityRenumberStatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='NO' WHERE EntityName='ApplicationUser'

		--Create back the Constraint once renumbering is done
		PRINT 'Creating Constraint'
		ALTER TABLE dbo.ApplicationUser
		ADD CONSTRAINT UQ_ApplicationUser_LastName_FirstName UNIQUE NONCLUSTERED
		(
			LastName
		,	FirstName
		)

		END
		END
		GO


