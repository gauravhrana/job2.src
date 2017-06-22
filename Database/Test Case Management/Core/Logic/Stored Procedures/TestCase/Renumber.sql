﻿ALTER Procedure dbo.TestCaseReNumber
(
		@TestCaseId				INT				= NULL	
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
CommonServices.dbo.EntityRenumberStatus WHERE EntityName='TestCase'

IF @RenumberingStatus = 'NO'
PRINT 'IN LOOP'
BEGIN
--Drop Unique constraint if exists
IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TestCase]')
	AND		name	= N'UQ_TestCase_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TestCase_ApplicationId_Name'
	ALTER	TABLE dbo.TestCase
	DROP	CONSTRAINT	UQ_TestCase_ApplicationId_Name
END

--Update renumbering status in EntityRenumberstatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='YES' WHERE EntityName='TestCase'

--Create table to track IDS
CREATE TABLE #IDTrack
(
			_OldID			INT					
		,	_NewID			INT					
		,	_HighNumberID   INT

)
-- Count number oif rows in TestCase
		DECLARE @Count AS INT = 0
		SELECT @Count = COUNT(*) FROM dbo.TestCase


		--Initialize loop iterator
		DECLARE @iCNT AS INT
		SET @iCNT = 1

		--declare and initialize high ID value
		DECLARE @highIDValue AS INT
		SET @highIDValue = 1000000

		--temp variable for old TestCaseid
		DECLARE @tmpTestCaseId AS INT

		Select * into #tmpTestCase from dbo.TestCase

		--this loop iterates and inserts high valued existing records into dbo.TestCase
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
	    DECLARE @SortOrder AS INT
	    SELECT  @SortOrder = MIN(SortOrder) from #tmpTestCase 
		--Select single record from TestCase table
		SELECT @tmpTestCaseId  = TestCaseId FROM #tmpTestCase a WHERE SortOrder = @SortOrder
		
		
		--Make a copy of record with highidvalue
		IF @tmpTestCaseId != @highIDValue AND @tmpTestCaseId > 0
		BEGIN
		INSERT INTO dbo.TestCase (TestCaseId, Name, Description, SortOrder, ApplicationId)
		SELECT @highIDValue,  Name, Description, SortOrder, ApplicationId
		FROM dbo.TestCase		
		WHERE TestCaseId = @tmpTestCaseId   
		
		--make an entry in IDTrack table
		--build IDTrack table
		INSERT INTO #IDTrack
		Select @tmpTestCaseId AS _OldID, @Seed AS _NewID, @highIDValue AS _HighNumberID
		
		INSERT INTO CommonServices.dbo.EntityIDTrack (EntityId, OldId, NewEntityId, HighNumberId, EntityName)
		VALUES( @SystemEntityTypeId, @tmpTestCaseId, @Seed, @highIDValue, 'TestCase')
	    
	    
		--Increment Seed, loop counter and highidvalue
		SET @Seed = @Seed + @Increment
		SET @highIDValue = @highIDValue + 1
		
		END
		SET @iCNT = @iCNT + 1
		--delete processed record from temp table of TestCase
		DELETE FROM #tmpTestCase WHERE TestCaseId = @tmpTestCaseId

		SET ROWCOUNT 0

		END 

		DROP TABLE #tmpTestCase
		
		Select * INTO #tmp1IDTrack from #IDTrack

		Select @Count = COUNT(*) from #tmp1IDTrack
		SET @iCNT = 0
		--This loop updates FK Tables to refer to high ID records of TestCase
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @HighNumberId AS INT
		SELECT @HighNumberId = MAX(_HighNumberiD)
		FROM #tmp1IDTrack
		--select old id from temp table of idtrack
		SELECT @tmpTestCaseId =  _OldID FROM #tmp1IDTrack WHERE _HighNumberId = @HighNumberId
		
		--Update Project table with highnumber TestCaseid in the place of old TestCaseid
	   --	UPDATE dbo.TestCaseXProject
	   --	SET TestCaseId = @HighNumberId	
	   --	WHERE TestCaseId = @tmpTestCaseId   
		
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from tamp table of idtrack
		DELETE FROM #tmp1IDTrack WHERE _OldID = @tmpTestCaseId AND _HighNumberID=@HighNumberId

		SET ROWCOUNT 0

		END
		
		
		--******************
		
		--copy records of IDTrack into temp table
		Select * INTO #tmp0IDTrack from #IDTrack
		SET @iCNT = 0

		--Declare temp variables for New and High TestCase Ids
		DECLARE @tmpNewTestCaseId AS INT
		DECLARE @tmpHighTestCaseId AS INT

		--Count the number of rows in TestCase table
		select @Count=Count(*) from dbo.TestCase
		
		--this loop deletes old records from dbo.TestCase
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from temp IDTrack table
		SELECT @tmpNewTestCaseId = MAX(_NewID)
		FROM #tmp0IDTrack
		
		--Get old and High Number Ids for the selected New Id from IDTrack table
		SELECT @tmpTestCaseId =  _OldID, @tmpHighTestCaseId = _HighNumberID  FROM #tmp0IDTrack WHERE _NewID = @tmpNewTestCaseId
		
		--Delete old record from the original table in DB
		IF @tmpTestCaseId != @tmpNewTestCaseId AND @tmpTestCaseId > 0
		BEGIN
		DELETE FROM dbo.TestCase WHERE TestCaseId = @tmpTestCaseId
		END

		-- Increment Loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp IDTrack table
		DELETE FROM #tmp0IDTrack WHERE _OldID = @tmpTestCaseId AND _NewID=@tmpNewTestCaseId

		SET ROWCOUNT 0
		END

		
		--make a copy of IDTrack table to iterate in loop
		Select * INTO #tmpIDTrack from #IDTrack
		
		--Set the count of number of rows in TestCase table
		select @Count=Count(*) from dbo.TestCase

		SET @iCNT = 1
		--This loop inserts existing records with new id values
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--select new TestCase id from idtrack table
		SELECT @tmpNewTestCaseId = MAX(_NewID)
		FROM #tmpIDTrack

		--select oldid from idtrack table
		SELECT @tmpTestCaseId =  _HighNumberID FROM #tmpIDTrack WHERE _NewID = @tmpNewTestCaseId
		--make a copy of existing record with new ID VALUE
		IF @tmpNewTestCaseId IS NOT NULL AND @tmpTestCaseId != @tmpNewTestCaseId AND NOT EXISTS (Select * from dbo.TestCase WHERE TestCaseId = @tmpNewTestCaseID)
		BEGIN
		INSERT INTO dbo.TestCase (TestCaseId, Name, Description, SortOrder, ApplicationId)
		SELECT @tmpNewTestCaseId,  Name, Description, SortOrder, ApplicationId
		FROM dbo.TestCase		
		WHERE TestCaseId = @tmpTestCaseId    
		END   
		
		SET @iCNT = @iCNT + 1
		
		--DELETE processed id record from temp tab;le
		DELETE FROM #tmpIDTrack WHERE _HighNumberID = @tmpTestCaseId AND _NewID=@tmpNewTestCaseId

		SET ROWCOUNT 0

		END

		
		DROP TABLE #tmpIDTrack

		--Copy IDTrack contents into temp table
		Select * INTO #tmp2IDTrack from #IDTrack

		--Set counter to number of rows in temp table of IDTrack
		Select @Count = COUNT(*) from #tmp2IDTrack
		SET @iCNT = 1
		--This loop updates FK Tables to refer to new ID records of TestCase
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @tmpHighId AS INT
		SELECT @tmpHighId = MAX(_HighNumberId)
		FROM #tmp2IDTrack

		--select new id from temp table of idtrack
		SELECT @tmpNewTestCaseId =  _NewID FROM #tmp2IDTrack WHERE _HighNumberID = @tmpHighId
		
		--Update Project table with new TestCaseid in the place of highnumber TestCaseid
	   --	UPDATE dbo.TestCaseXProject
	   --	SET TestCaseId = @tmpNewTestCaseId	
	   --	WHERE TestCaseId = @tmpHighId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from temp table of idtrack
		DELETE FROM #tmp2IDTrack WHERE _NewID = @tmpNewTestCaseId AND _HighNumberID=@tmpHighId

		SET ROWCOUNT 0

		END

		Select * INTO #tmp3IDTrack from #IDTrack
		SET @iCNT = 1
		
		--Set counter to number of rows in TestCase table
		select @Count=Count(*) from dbo.TestCase
		
		--Select * from dbo.TestCase
		--this loop deleteshighnumbered records from dbo.TestCase
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from IDTrack table
		SELECT @tmpNewTestCaseId = MAX(_NewID)
		FROM #tmp3IDTrack

		--Get old and high number Ids for the selected New Id value
		SELECT @tmpTestCaseId =  _OldID, @tmpHighTestCaseId = _HighNumberID  FROM #tmp3IDTrack WHERE _NewID = @tmpNewTestCaseId
		
		--Delete High numbered Record from original table
		DELETE FROM dbo.TestCase WHERE TestCaseId = @tmpHighTestCaseId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp table of IDTrack
		DELETE FROM #tmp3IDTrack WHERE _OldID = @tmpTestCaseId AND _NewID=@tmpNewTestCaseId

		SET ROWCOUNT 0
		END

		--Update status in EntityRenumberStatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='NO' WHERE EntityName='TestCase'

		--Create back the Constraint once renumbering is done
		PRINT 'Creating Constraint'
		ALTER TABLE dbo.TestCase
		ADD CONSTRAINT UQ_TestCase_ApplicationId_Name UNIQUE NONCLUSTERED
		(
				ApplicationId
			,	Name	
		)

		END
		END
GO
