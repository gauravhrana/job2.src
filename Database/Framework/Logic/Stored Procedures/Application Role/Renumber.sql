IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleReNumber')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleReNumber'
	DROP   Procedure ApplicationRoleReNumber
END
GO

PRINT 'Creating Procedure ApplicationRoleReNumber'
GO

/******************************************************************************

**		File: 
**		Name: ApplicationRoleReNumber
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
			EXEC ApplicationRoleReOrder	 NULL	, NULL	, NULL
			EXEC ApplicationRoleReOrder	 NULL	, 'K'	, NULL
			EXEC ApplicationRoleReOrder	 1		, 'K'	, NULL
			EXEC ApplicationRoleReOrder   1		, NULL	, NULL
			EXEC ApplicationRoleReOrder   NULL	, NULL	, 'W'

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
Create Procedure dbo.ApplicationRoleReNumber
(
		@ApplicationRoleId		INT				= NULL	
	,	@Direction				INT				= NULL	
	,	@Seed					INT				= NULL	
	,	@Increment				INT				= NULL	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityTypeId		INT				= 300
)
AS
BEGIN
DECLARE @RenumberingStatus AS VARCHAR(50)
SELECT @RenumberingStatus = IsUnderRenumbering from
CommonServices.dbo.EntityRenumberStatus WHERE EntityName='ApplicationRole'

IF @RenumberingStatus = 'NO'
PRINT 'IN LOOP'
BEGIN
--Drop Unique constraint if exists
IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ApplicationRole]')
	AND		name	= N'UQ_ApplicationRole_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationRole_ApplicationId_Name'
	ALTER	TABLE dbo.ApplicationRole
	DROP	CONSTRAINT	UQ_ApplicationRole_ApplicationId_Name
END

--Update renumbering status in EntityRenumberstatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='YES' WHERE EntityName='ApplicationRole'

--Create table to track IDS
CREATE TABLE #IDTrack
(
			_OldID			INT					
		,	_NewID			INT					
		,	_HighNumberID   INT

)
-- Count number oif rows in ApplicationRole
		DECLARE @Count AS INT = 0
		SELECT @Count = COUNT(*) FROM dbo.ApplicationRole


		--Initialize loop iterator
		DECLARE @iCNT AS INT
		SET @iCNT = 1

		--declare and initialize high ID value
		DECLARE @highIDValue AS INT
		SET @highIDValue = 1000000

		--temp variable for old ApplicationRoleid
		DECLARE @tmpApplicationRoleId AS INT

		Select * into #tmpApplicationRole from dbo.ApplicationRole

		--this loop iterates and inserts high valued existing records into dbo.ApplicationRole
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
	    DECLARE @SortOrder AS INT
	    SELECT  @SortOrder = MIN(SortOrder) from #tmpApplicationRole 
		--Select single record from ApplicationRole table
		SELECT @tmpApplicationRoleId  = ApplicationRoleId FROM #tmpApplicationRole a WHERE SortOrder = @SortOrder
		
		
		--Make a copy of record with highidvalue
		IF @tmpApplicationRoleId != @highIDValue AND @tmpApplicationRoleId > 0
		BEGIN
		INSERT INTO dbo.ApplicationRole (ApplicationRoleId, Name, Description, SortOrder, ApplicationId)
		SELECT @highIDValue,  Name, Description, SortOrder, ApplicationId
		FROM dbo.ApplicationRole		
		WHERE ApplicationRoleId = @tmpApplicationRoleId   
		
		--make an entry in IDTrack table
		--build IDTrack table
		INSERT INTO #IDTrack
		Select @tmpApplicationRoleId AS _OldID, @Seed AS _NewID, @highIDValue AS _HighNumberID
		
		INSERT INTO CommonServices.dbo.EntityIDTrack (EntityId, OldId, NewEntityId, HighNumberId, EntityName)
		VALUES( @SystemEntityTypeId, @tmpApplicationRoleId, @Seed, @highIDValue, 'ApplicationRole')
	    
	    
		--Increment Seed, loop counter and highidvalue
		SET @Seed = @Seed + @Increment
		SET @highIDValue = @highIDValue + 1
		
		END
		SET @iCNT = @iCNT + 1
		--delete processed record from temp table of ApplicationRole
		DELETE FROM #tmpApplicationRole WHERE ApplicationRoleId = @tmpApplicationRoleId

		SET ROWCOUNT 0

		END 

		DROP TABLE #tmpApplicationRole
		
		Select * INTO #tmp1IDTrack from #IDTrack

		Select @Count = COUNT(*) from #tmp1IDTrack
		SET @iCNT = 0
		--This loop updates FK Tables to refer to high ID records of ApplicationRole
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @HighNumberId AS INT
		SELECT @HighNumberId = MAX(_HighNumberiD)
		FROM #tmp1IDTrack
		--select old id from temp table of idtrack
		SELECT @tmpApplicationRoleId =  _OldID FROM #tmp1IDTrack WHERE _HighNumberId = @HighNumberId
		
		--Update Project table with highnumber ApplicationRoleid in the place of old ApplicationRoleid
		UPDATE dbo.ApplicationOperationXApplicationRole
		SET ApplicationRoleId = @HighNumberId	
		WHERE ApplicationRoleId = @tmpApplicationRoleId   

		UPDATE dbo.ApplicationUserXApplicationRole
		SET ApplicationRoleId = @HighNumberId	
		WHERE ApplicationRoleId = @tmpApplicationRoleId
		
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from tamp table of idtrack
		DELETE FROM #tmp1IDTrack WHERE _OldID = @tmpApplicationRoleId AND _HighNumberID=@HighNumberId

		SET ROWCOUNT 0

		END
		
		
		--******************
		
		--copy records of IDTrack into temp table
		Select * INTO #tmp0IDTrack from #IDTrack
		SET @iCNT = 0

		--Declare temp variables for New and High ApplicationRole Ids
		DECLARE @tmpNewApplicationRoleId AS INT
		DECLARE @tmpHighApplicationRoleId AS INT

		--Count the number of rows in ApplicationRole table
		select @Count=Count(*) from dbo.ApplicationRole
		
		--this loop deletes old records from dbo.ApplicationRole
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from temp IDTrack table
		SELECT @tmpNewApplicationRoleId = MAX(_NewID)
		FROM #tmp0IDTrack
		
		--Get old and High Number Ids for the selected New Id from IDTrack table
		SELECT @tmpApplicationRoleId =  _OldID, @tmpHighApplicationRoleId = _HighNumberID  FROM #tmp0IDTrack WHERE _NewID = @tmpNewApplicationRoleId
		
		--Delete old record from the original table in DB
		IF @tmpApplicationRoleId != @tmpNewApplicationRoleId AND @tmpApplicationRoleId > 0
		BEGIN
		DELETE FROM dbo.ApplicationRole WHERE ApplicationRoleId = @tmpApplicationRoleId
		END

		-- Increment Loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp IDTrack table
		DELETE FROM #tmp0IDTrack WHERE _OldID = @tmpApplicationRoleId AND _NewID=@tmpNewApplicationRoleId

		SET ROWCOUNT 0
		END

		
		--make a copy of IDTrack table to iterate in loop
		Select * INTO #tmpIDTrack from #IDTrack
		
		--Set the count of number of rows in ApplicationRole table
		select @Count=Count(*) from dbo.ApplicationRole

		SET @iCNT = 1
		--This loop inserts existing records with new id values
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--select new ApplicationRole id from idtrack table
		SELECT @tmpNewApplicationRoleId = MAX(_NewID)
		FROM #tmpIDTrack

		--select oldid from idtrack table
		SELECT @tmpApplicationRoleId =  _HighNumberID FROM #tmpIDTrack WHERE _NewID = @tmpNewApplicationRoleId
		--make a copy of existing record with new ID VALUE
		IF @tmpNewApplicationRoleId IS NOT NULL AND @tmpApplicationRoleId != @tmpNewApplicationRoleId AND NOT EXISTS (Select * from dbo.ApplicationRole WHERE ApplicationRoleId = @tmpNewApplicationRoleID)
		BEGIN
		INSERT INTO dbo.ApplicationRole (ApplicationRoleId, Name, Description, SortOrder, ApplicationId)
		SELECT @tmpNewApplicationRoleId,  Name, Description, SortOrder, ApplicationId
		FROM dbo.ApplicationRole		
		WHERE ApplicationRoleId = @tmpApplicationRoleId    
		END   
		
		SET @iCNT = @iCNT + 1
		
		--DELETE processed id record from temp tab;le
		DELETE FROM #tmpIDTrack WHERE _HighNumberID = @tmpApplicationRoleId AND _NewID=@tmpNewApplicationRoleId

		SET ROWCOUNT 0

		END

		
		DROP TABLE #tmpIDTrack

		--Copy IDTrack contents into temp table
		Select * INTO #tmp2IDTrack from #IDTrack

		--Set counter to number of rows in temp table of IDTrack
		Select @Count = COUNT(*) from #tmp2IDTrack
		SET @iCNT = 1
		--This loop updates FK Tables to refer to new ID records of ApplicationRole
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @tmpHighId AS INT
		SELECT @tmpHighId = MAX(_HighNumberId)
		FROM #tmp2IDTrack

		--select new id from temp table of idtrack
		SELECT @tmpNewApplicationRoleId =  _NewID FROM #tmp2IDTrack WHERE _HighNumberID = @tmpHighId
		
		--Update Project table with new ApplicationRoleid in the place of highnumber ApplicationRoleid
		UPDATE dbo.ApplicationOperationXApplicationRole
		SET ApplicationRoleId = @tmpNewApplicationRoleId	
		WHERE ApplicationRoleId = @tmpHighId   

		UPDATE dbo.ApplicationUserXApplicationRole
		SET ApplicationRoleId = @tmpNewApplicationRoleId	
		WHERE ApplicationRoleId = @tmpHighId 
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from temp table of idtrack
		DELETE FROM #tmp2IDTrack WHERE _NewID = @tmpNewApplicationRoleId AND _HighNumberID=@tmpHighId

		SET ROWCOUNT 0

		END

		Select * INTO #tmp3IDTrack from #IDTrack
		SET @iCNT = 1
		
		--Set counter to number of rows in ApplicationRole table
		select @Count=Count(*) from dbo.ApplicationRole
		
		--Select * from dbo.ApplicationRole
		--this loop deleteshighnumbered records from dbo.ApplicationRole
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from IDTrack table
		SELECT @tmpNewApplicationRoleId = MAX(_NewID)
		FROM #tmp3IDTrack

		--Get old and high number Ids for the selected New Id value
		SELECT @tmpApplicationRoleId =  _OldID, @tmpHighApplicationRoleId = _HighNumberID  FROM #tmp3IDTrack WHERE _NewID = @tmpNewApplicationRoleId
		
		--Delete High numbered Record from original table
		DELETE FROM dbo.ApplicationRole WHERE ApplicationRoleId = @tmpHighApplicationRoleId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp table of IDTrack
		DELETE FROM #tmp3IDTrack WHERE _OldID = @tmpApplicationRoleId AND _NewID=@tmpNewApplicationRoleId

		SET ROWCOUNT 0
		END

		--Update status in EntityRenumberStatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='NO' WHERE EntityName='ApplicationRole'

		--Create back the Constraint once renumbering is done
		PRINT 'Creating Constraint'
		ALTER TABLE dbo.ApplicationRole
		ADD CONSTRAINT UQ_ApplicationRole_ApplicationId_Name UNIQUE NONCLUSTERED
		(
				ApplicationId
			,	Name	
		)

		END
		END
		GO


