ALTER Procedure dbo.TestRunReNumber
(
		@TestRunId				INT				= NULL	
	,	@Direction				INT				= NULL	
	,	@Seed					INT				= NULL	
	,	@Increment				INT				= NULL	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityTypeId		INT				= 3000
)
AS
BEGIN
DECLARE @RenumberingStatus AS VARCHAR(50)
SELECT @RenumberingStatus = IsUnderRenumbering from
CommonServices.dbo.EntityRenumberStatus WHERE EntityName='TestRun'

IF @RenumberingStatus = 'NO'
PRINT 'IN LOOP'
BEGIN
--Drop Unique constraint if exists
IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TestRun]')
	AND		name	= N'UQ_TestRun_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TestRun_ApplicationId_Name'
	ALTER	TABLE dbo.TestRun
	DROP	CONSTRAINT	UQ_TestRun_ApplicationId_Name
END

--Update renumbering status in EntityRenumberstatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='YES' WHERE EntityName='TestRun'

--Create table to track IDS
CREATE TABLE #IDTrack
(
			_OldID			INT					
		,	_NewID			INT					
		,	_HighNumberID   INT

)
-- Count number oif rows in TestRun
		DECLARE @Count AS INT = 0
		SELECT @Count = COUNT(*) FROM dbo.TestRun


		--Initialize loop iterator
		DECLARE @iCNT AS INT
		SET @iCNT = 1

		--declare and initialize high ID value
		DECLARE @highIDValue AS INT
		SET @highIDValue = 1000000

		--temp variable for old TestRunid
		DECLARE @tmpTestRunId AS INT

		Select * into #tmpTestRun from dbo.TestRun

		--this loop iterates and inserts high valued existing records into dbo.TestRun
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
	    DECLARE @SortOrder AS INT
	    SELECT  @SortOrder = MIN(SortOrder) from #tmpTestRun 
		--Select single record from TestRun table
		SELECT @tmpTestRunId  = TestRunId FROM #tmpTestRun a WHERE SortOrder = @SortOrder
		
		
		--Make a copy of record with highidvalue
		IF @tmpTestRunId != @highIDValue AND @tmpTestRunId > 0
		BEGIN
		INSERT INTO dbo.TestRun (TestRunId, Name, Description, SortOrder, ApplicationId)
		SELECT @highIDValue,  Name, Description, SortOrder, ApplicationId
		FROM dbo.TestRun		
		WHERE TestRunId = @tmpTestRunId   
		
		--make an entry in IDTrack table
		--build IDTrack table
		INSERT INTO #IDTrack
		Select @tmpTestRunId AS _OldID, @Seed AS _NewID, @highIDValue AS _HighNumberID
		
		INSERT INTO CommonServices.dbo.EntityIDTrack (EntityId, OldId, NewEntityId, HighNumberId, EntityName)
		VALUES( @SystemEntityTypeId, @tmpTestRunId, @Seed, @highIDValue, 'TestRun')
	    
	    
		--Increment Seed, loop counter and highidvalue
		SET @Seed = @Seed + @Increment
		SET @highIDValue = @highIDValue + 1
		
		END
		SET @iCNT = @iCNT + 1
		--delete processed record from temp table of TestRun
		DELETE FROM #tmpTestRun WHERE TestRunId = @tmpTestRunId

		SET ROWCOUNT 0

		END 

		DROP TABLE #tmpTestRun
		
		Select * INTO #tmp1IDTrack from #IDTrack

		Select @Count = COUNT(*) from #tmp1IDTrack
		SET @iCNT = 0
		--This loop updates FK Tables to refer to high ID records of TestRun
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @HighNumberId AS INT
		SELECT @HighNumberId = MAX(_HighNumberiD)
		FROM #tmp1IDTrack
		--select old id from temp table of idtrack
		SELECT @tmpTestRunId =  _OldID FROM #tmp1IDTrack WHERE _HighNumberId = @HighNumberId
		
		--Update Project table with highnumber TestRunid in the place of old TestRunid
	   --	UPDATE dbo.TestRunXProject
	   --	SET TestRunId = @HighNumberId	
	   --	WHERE TestRunId = @tmpTestRunId   
		
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from tamp table of idtrack
		DELETE FROM #tmp1IDTrack WHERE _OldID = @tmpTestRunId AND _HighNumberID=@HighNumberId

		SET ROWCOUNT 0

		END
		
		
		--******************
		
		--copy records of IDTrack into temp table
		Select * INTO #tmp0IDTrack from #IDTrack
		SET @iCNT = 0

		--Declare temp variables for New and High TestRun Ids
		DECLARE @tmpNewTestRunId AS INT
		DECLARE @tmpHighTestRunId AS INT

		--Count the number of rows in TestRun table
		select @Count=Count(*) from dbo.TestRun
		
		--this loop deletes old records from dbo.TestRun
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from temp IDTrack table
		SELECT @tmpNewTestRunId = MAX(_NewID)
		FROM #tmp0IDTrack
		
		--Get old and High Number Ids for the selected New Id from IDTrack table
		SELECT @tmpTestRunId =  _OldID, @tmpHighTestRunId = _HighNumberID  FROM #tmp0IDTrack WHERE _NewID = @tmpNewTestRunId
		
		--Delete old record from the original table in DB
		IF @tmpTestRunId != @tmpNewTestRunId AND @tmpTestRunId > 0
		BEGIN
		DELETE FROM dbo.TestRun WHERE TestRunId = @tmpTestRunId
		END

		-- Increment Loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp IDTrack table
		DELETE FROM #tmp0IDTrack WHERE _OldID = @tmpTestRunId AND _NewID=@tmpNewTestRunId

		SET ROWCOUNT 0
		END

		
		--make a copy of IDTrack table to iterate in loop
		Select * INTO #tmpIDTrack from #IDTrack
		
		--Set the count of number of rows in TestRun table
		select @Count=Count(*) from dbo.TestRun

		SET @iCNT = 1
		--This loop inserts existing records with new id values
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--select new TestRun id from idtrack table
		SELECT @tmpNewTestRunId = MAX(_NewID)
		FROM #tmpIDTrack

		--select oldid from idtrack table
		SELECT @tmpTestRunId =  _HighNumberID FROM #tmpIDTrack WHERE _NewID = @tmpNewTestRunId
		--make a copy of existing record with new ID VALUE
		IF @tmpNewTestRunId IS NOT NULL AND @tmpTestRunId != @tmpNewTestRunId AND NOT EXISTS (Select * from dbo.TestRun WHERE TestRunId = @tmpNewTestRunID)
		BEGIN
		INSERT INTO dbo.TestRun (TestRunId, Name, Description, SortOrder, ApplicationId)
		SELECT @tmpNewTestRunId,  Name, Description, SortOrder, ApplicationId
		FROM dbo.TestRun		
		WHERE TestRunId = @tmpTestRunId    
		END   
		
		SET @iCNT = @iCNT + 1
		
		--DELETE processed id record from temp tab;le
		DELETE FROM #tmpIDTrack WHERE _HighNumberID = @tmpTestRunId AND _NewID=@tmpNewTestRunId

		SET ROWCOUNT 0

		END

		
		DROP TABLE #tmpIDTrack

		--Copy IDTrack contents into temp table
		Select * INTO #tmp2IDTrack from #IDTrack

		--Set counter to number of rows in temp table of IDTrack
		Select @Count = COUNT(*) from #tmp2IDTrack
		SET @iCNT = 1
		--This loop updates FK Tables to refer to new ID records of TestRun
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @tmpHighId AS INT
		SELECT @tmpHighId = MAX(_HighNumberId)
		FROM #tmp2IDTrack

		--select new id from temp table of idtrack
		SELECT @tmpNewTestRunId =  _NewID FROM #tmp2IDTrack WHERE _HighNumberID = @tmpHighId
		
		--Update Project table with new TestRunid in the place of highnumber TestRunid
	   --	UPDATE dbo.TestRunXProject
	   --	SET TestRunId = @tmpNewTestRunId	
	   --	WHERE TestRunId = @tmpHighId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from temp table of idtrack
		DELETE FROM #tmp2IDTrack WHERE _NewID = @tmpNewTestRunId AND _HighNumberID=@tmpHighId

		SET ROWCOUNT 0

		END

		Select * INTO #tmp3IDTrack from #IDTrack
		SET @iCNT = 1
		
		--Set counter to number of rows in TestRun table
		select @Count=Count(*) from dbo.TestRun
		
		--Select * from dbo.TestRun
		--this loop deleteshighnumbered records from dbo.TestRun
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from IDTrack table
		SELECT @tmpNewTestRunId = MAX(_NewID)
		FROM #tmp3IDTrack

		--Get old and high number Ids for the selected New Id value
		SELECT @tmpTestRunId =  _OldID, @tmpHighTestRunId = _HighNumberID  FROM #tmp3IDTrack WHERE _NewID = @tmpNewTestRunId
		
		--Delete High numbered Record from original table
		DELETE FROM dbo.TestRun WHERE TestRunId = @tmpHighTestRunId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp table of IDTrack
		DELETE FROM #tmp3IDTrack WHERE _OldID = @tmpTestRunId AND _NewID=@tmpNewTestRunId

		SET ROWCOUNT 0
		END

		--Update status in EntityRenumberStatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='NO' WHERE EntityName='TestRun'

		--Create back the Constraint once renumbering is done
		PRINT 'Creating Constraint'
		ALTER TABLE dbo.TestRun
		ADD CONSTRAINT UQ_TestRun_ApplicationId_Name UNIQUE NONCLUSTERED
		(
				ApplicationId
			,	Name	
		)

		END
		END
GO

