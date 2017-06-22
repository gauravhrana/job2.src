CREATE Procedure dbo.TestSuiteReNumber
(
		@TestSuiteId				INT				= NULL	
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
CommonServices.dbo.EntityRenumberStatus WHERE EntityName='TestSuite'

IF @RenumberingStatus = 'NO'
PRINT 'IN LOOP'
BEGIN
--Drop Unique constraint if exists
IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TestSuite]')
	AND		name	= N'UQ_TestSuite_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TestSuite_ApplicationId_Name'
	ALTER	TABLE dbo.TestSuite
	DROP	CONSTRAINT	UQ_TestSuite_ApplicationId_Name
END

--Update renumbering status in EntityRenumberstatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='YES' WHERE EntityName='TestSuite'

--Create table to track IDS
CREATE TABLE #IDTrack
(
			_OldID			INT					
		,	_NewID			INT					
		,	_HighNumberID   INT

)
-- Count number oif rows in TestSuite
		DECLARE @Count AS INT = 0
		SELECT @Count = COUNT(*) FROM dbo.TestSuite


		--Initialize loop iterator
		DECLARE @iCNT AS INT
		SET @iCNT = 1

		--declare and initialize high ID value
		DECLARE @highIDValue AS INT
		SET @highIDValue = 1000000

		--temp variable for old TestSuiteid
		DECLARE @tmpTestSuiteId AS INT

		Select * into #tmpTestSuite from dbo.TestSuite

		--this loop iterates and inserts high valued existing records into dbo.TestSuite
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
	    DECLARE @SortOrder AS INT
	    SELECT  @SortOrder = MIN(SortOrder) from #tmpTestSuite 
		--Select single record from TestSuite table
		SELECT @tmpTestSuiteId  = TestSuiteId FROM #tmpTestSuite a WHERE SortOrder = @SortOrder
		
		
		--Make a copy of record with highidvalue
		IF @tmpTestSuiteId != @highIDValue AND @tmpTestSuiteId > 0
		BEGIN
		INSERT INTO dbo.TestSuite (TestSuiteId, Name, Description, SortOrder, ApplicationId)
		SELECT @highIDValue,  Name, Description, SortOrder, ApplicationId
		FROM dbo.TestSuite		
		WHERE TestSuiteId = @tmpTestSuiteId   
		
		--make an entry in IDTrack table
		--build IDTrack table
		INSERT INTO #IDTrack
		Select @tmpTestSuiteId AS _OldID, @Seed AS _NewID, @highIDValue AS _HighNumberID
		
		INSERT INTO CommonServices.dbo.EntityIDTrack (EntityId, OldId, NewEntityId, HighNumberId, EntityName)
		VALUES( @SystemEntityTypeId, @tmpTestSuiteId, @Seed, @highIDValue, 'TestSuite')
	    
	    
		--Increment Seed, loop counter and highidvalue
		SET @Seed = @Seed + @Increment
		SET @highIDValue = @highIDValue + 1
		
		END
		SET @iCNT = @iCNT + 1
		--delete processed record from temp table of TestSuite
		DELETE FROM #tmpTestSuite WHERE TestSuiteId = @tmpTestSuiteId

		SET ROWCOUNT 0

		END 

		DROP TABLE #tmpTestSuite
		
		Select * INTO #tmp1IDTrack from #IDTrack

		Select @Count = COUNT(*) from #tmp1IDTrack
		SET @iCNT = 0
		--This loop updates FK Tables to refer to high ID records of TestSuite
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @HighNumberId AS INT
		SELECT @HighNumberId = MAX(_HighNumberiD)
		FROM #tmp1IDTrack
		--select old id from temp table of idtrack
		SELECT @tmpTestSuiteId =  _OldID FROM #tmp1IDTrack WHERE _HighNumberId = @HighNumberId
		
		--Update Project table with highnumber TestSuiteid in the place of old TestSuiteid
	   --	UPDATE dbo.TestSuiteXProject
	   --	SET TestSuiteId = @HighNumberId	
	   --	WHERE TestSuiteId = @tmpTestSuiteId   
		
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from tamp table of idtrack
		DELETE FROM #tmp1IDTrack WHERE _OldID = @tmpTestSuiteId AND _HighNumberID=@HighNumberId

		SET ROWCOUNT 0

		END
		
		
		--******************
		
		--copy records of IDTrack into temp table
		Select * INTO #tmp0IDTrack from #IDTrack
		SET @iCNT = 0

		--Declare temp variables for New and High TestSuite Ids
		DECLARE @tmpNewTestSuiteId AS INT
		DECLARE @tmpHighTestSuiteId AS INT

		--Count the number of rows in TestSuite table
		select @Count=Count(*) from dbo.TestSuite
		
		--this loop deletes old records from dbo.TestSuite
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from temp IDTrack table
		SELECT @tmpNewTestSuiteId = MAX(_NewID)
		FROM #tmp0IDTrack
		
		--Get old and High Number Ids for the selected New Id from IDTrack table
		SELECT @tmpTestSuiteId =  _OldID, @tmpHighTestSuiteId = _HighNumberID  FROM #tmp0IDTrack WHERE _NewID = @tmpNewTestSuiteId
		
		--Delete old record from the original table in DB
		IF @tmpTestSuiteId != @tmpNewTestSuiteId AND @tmpTestSuiteId > 0
		BEGIN
		DELETE FROM dbo.TestSuite WHERE TestSuiteId = @tmpTestSuiteId
		END

		-- Increment Loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp IDTrack table
		DELETE FROM #tmp0IDTrack WHERE _OldID = @tmpTestSuiteId AND _NewID=@tmpNewTestSuiteId

		SET ROWCOUNT 0
		END

		
		--make a copy of IDTrack table to iterate in loop
		Select * INTO #tmpIDTrack from #IDTrack
		
		--Set the count of number of rows in TestSuite table
		select @Count=Count(*) from dbo.TestSuite

		SET @iCNT = 1
		--This loop inserts existing records with new id values
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--select new TestSuite id from idtrack table
		SELECT @tmpNewTestSuiteId = MAX(_NewID)
		FROM #tmpIDTrack

		--select oldid from idtrack table
		SELECT @tmpTestSuiteId =  _HighNumberID FROM #tmpIDTrack WHERE _NewID = @tmpNewTestSuiteId
		--make a copy of existing record with new ID VALUE
		IF @tmpNewTestSuiteId IS NOT NULL AND @tmpTestSuiteId != @tmpNewTestSuiteId AND NOT EXISTS (Select * from dbo.TestSuite WHERE TestSuiteId = @tmpNewTestSuiteID)
		BEGIN
		INSERT INTO dbo.TestSuite (TestSuiteId, Name, Description, SortOrder, ApplicationId)
		SELECT @tmpNewTestSuiteId,  Name, Description, SortOrder, ApplicationId
		FROM dbo.TestSuite		
		WHERE TestSuiteId = @tmpTestSuiteId    
		END   
		
		SET @iCNT = @iCNT + 1
		
		--DELETE processed id record from temp tab;le
		DELETE FROM #tmpIDTrack WHERE _HighNumberID = @tmpTestSuiteId AND _NewID=@tmpNewTestSuiteId

		SET ROWCOUNT 0

		END

		
		DROP TABLE #tmpIDTrack

		--Copy IDTrack contents into temp table
		Select * INTO #tmp2IDTrack from #IDTrack

		--Set counter to number of rows in temp table of IDTrack
		Select @Count = COUNT(*) from #tmp2IDTrack
		SET @iCNT = 1
		--This loop updates FK Tables to refer to new ID records of TestSuite
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @tmpHighId AS INT
		SELECT @tmpHighId = MAX(_HighNumberId)
		FROM #tmp2IDTrack

		--select new id from temp table of idtrack
		SELECT @tmpNewTestSuiteId =  _NewID FROM #tmp2IDTrack WHERE _HighNumberID = @tmpHighId
		
		--Update Project table with new TestSuiteid in the place of highnumber TestSuiteid
	   --	UPDATE dbo.TestSuiteXProject
	   --	SET TestSuiteId = @tmpNewTestSuiteId	
	   --	WHERE TestSuiteId = @tmpHighId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from temp table of idtrack
		DELETE FROM #tmp2IDTrack WHERE _NewID = @tmpNewTestSuiteId AND _HighNumberID=@tmpHighId

		SET ROWCOUNT 0

		END

		Select * INTO #tmp3IDTrack from #IDTrack
		SET @iCNT = 1
		
		--Set counter to number of rows in TestSuite table
		select @Count=Count(*) from dbo.TestSuite
		
		--Select * from dbo.TestSuite
		--this loop deleteshighnumbered records from dbo.TestSuite
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from IDTrack table
		SELECT @tmpNewTestSuiteId = MAX(_NewID)
		FROM #tmp3IDTrack

		--Get old and high number Ids for the selected New Id value
		SELECT @tmpTestSuiteId =  _OldID, @tmpHighTestSuiteId = _HighNumberID  FROM #tmp3IDTrack WHERE _NewID = @tmpNewTestSuiteId
		
		--Delete High numbered Record from original table
		DELETE FROM dbo.TestSuite WHERE TestSuiteId = @tmpHighTestSuiteId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp table of IDTrack
		DELETE FROM #tmp3IDTrack WHERE _OldID = @tmpTestSuiteId AND _NewID=@tmpNewTestSuiteId

		SET ROWCOUNT 0
		END

		--Update status in EntityRenumberStatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='NO' WHERE EntityName='TestSuite'

		--Create back the Constraint once renumbering is done
		PRINT 'Creating Constraint'
		ALTER TABLE dbo.TestSuite
		ADD CONSTRAINT UQ_TestSuite_ApplicationId_Name UNIQUE NONCLUSTERED
		(
				ApplicationId
			,	Name	
		)

		END
		END
GO

