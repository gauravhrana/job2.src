IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationReNumber')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationReNumber'
	DROP   Procedure ApplicationOperationReNumber
END
GO

PRINT 'Creating Procedure ApplicationOperationReNumber'
GO

/******************************************************************************

**		File: 
**		Name: ApplicationOperationReNumber
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
			EXEC ApplicationOperationReOrder	 NULL	, NULL	, NULL
			EXEC ApplicationOperationReOrder	 NULL	, 'K'	, NULL
			EXEC ApplicationOperationReOrder	 1		, 'K'	, NULL
			EXEC ApplicationOperationReOrder   1		, NULL	, NULL
			EXEC ApplicationOperationReOrder   NULL	, NULL	, 'W'

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
Create Procedure dbo.ApplicationOperationReNumber
(
		@ApplicationOperationId				INT				= NULL	
	,	@Direction				INT				= NULL	
	,	@Seed					INT				= NULL	
	,	@Increment				INT				= NULL	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityTypeId		INT				= 6700
)
AS
BEGIN
DECLARE @RenumberingStatus AS VARCHAR(50)
SELECT @RenumberingStatus = IsUnderRenumbering from
CommonServices.dbo.EntityRenumberStatus WHERE EntityName='ApplicationOperation'

IF @RenumberingStatus = 'NO'
PRINT 'IN LOOP'
BEGIN
--Drop Unique constraint if exists
IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ApplicationOperation]')
	AND		name	= N'UQ_ApplicationOperation_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationOperation_ApplicationId_Name'
	ALTER	TABLE dbo.ApplicationOperation
	DROP	CONSTRAINT	UQ_ApplicationOperation_ApplicationId_Name
END

--Update renumbering status in EntityRenumberstatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='YES' WHERE EntityName='ApplicationOperation'

--Create table to track IDS
CREATE TABLE #IDTrack
(
			_OldID			INT					
		,	_NewID			INT					
		,	_HighNumberID   INT

)
-- Count number oif rows in ApplicationOperation
		DECLARE @Count AS INT = 0
		SELECT @Count = COUNT(*) FROM dbo.ApplicationOperation


		--Initialize loop iterator
		DECLARE @iCNT AS INT
		SET @iCNT = 1

		--declare and initialize high ID value
		DECLARE @highIDValue AS INT
		SET @highIDValue = 1000000

		--temp variable for old ApplicationOperationid
		DECLARE @tmpApplicationOperationId AS INT

		Select * into #tmpApplicationOperation from dbo.ApplicationOperation

		--this loop iterates and inserts high valued existing records into dbo.ApplicationOperation
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
	    DECLARE @SortOrder AS INT
	    SELECT  @SortOrder = MIN(SortOrder) from #tmpApplicationOperation 
		--Select single record from ApplicationOperation table
		SELECT @tmpApplicationOperationId  = ApplicationOperationId FROM #tmpApplicationOperation a WHERE SortOrder = @SortOrder
		
		
		--Make a copy of record with highidvalue
		IF @tmpApplicationOperationId != @highIDValue AND @tmpApplicationOperationId > 0
		BEGIN
		INSERT INTO dbo.ApplicationOperation (ApplicationOperationId, Name, Description, SortOrder, ApplicationId, SystemEntityTypeId, OperationValue)
		SELECT @highIDValue,  Name, Description, SortOrder, ApplicationId, SystemEntityTypeId, OperationValue
		FROM dbo.ApplicationOperation		
		WHERE ApplicationOperationId = @tmpApplicationOperationId   
		
		--make an entry in IDTrack table
		--build IDTrack table
		INSERT INTO #IDTrack
		Select @tmpApplicationOperationId AS _OldID, @Seed AS _NewID, @highIDValue AS _HighNumberID
		
		INSERT INTO CommonServices.dbo.EntityIDTrack (EntityId, OldId, NewEntityId, HighNumberId, EntityName)
		VALUES( @SystemEntityTypeId, @tmpApplicationOperationId, @Seed, @highIDValue, 'ApplicationOperation')
	    
	    
		--Increment Seed, loop counter and highidvalue
		SET @Seed = @Seed + @Increment
		SET @highIDValue = @highIDValue + 1
		
		END
		SET @iCNT = @iCNT + 1
		--delete processed record from temp table of ApplicationOperation
		DELETE FROM #tmpApplicationOperation WHERE ApplicationOperationId = @tmpApplicationOperationId

		SET ROWCOUNT 0

		END 

		DROP TABLE #tmpApplicationOperation
		
		Select * INTO #tmp1IDTrack from #IDTrack

		Select @Count = COUNT(*) from #tmp1IDTrack
		SET @iCNT = 0
		--This loop updates FK Tables to refer to high ID records of ApplicationOperation
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @HighNumberId AS INT
		SELECT @HighNumberId = MAX(_HighNumberiD)
		FROM #tmp1IDTrack
		--select old id from temp table of idtrack
		SELECT @tmpApplicationOperationId =  _OldID FROM #tmp1IDTrack WHERE _HighNumberId = @HighNumberId
		
		--Update Project table with highnumber ApplicationOperationid in the place of old ApplicationOperationid
		UPDATE dbo.ApplicationOperationXApplicationRole
		SET ApplicationOperationId = @HighNumberId	
		WHERE ApplicationOperationId = @tmpApplicationOperationId   
		
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from tamp table of idtrack
		DELETE FROM #tmp1IDTrack WHERE _OldID = @tmpApplicationOperationId AND _HighNumberID=@HighNumberId

		SET ROWCOUNT 0

		END
		
		
		--******************
		
		--copy records of IDTrack into temp table
		Select * INTO #tmp0IDTrack from #IDTrack
		SET @iCNT = 0

		--Declare temp variables for New and High ApplicationOperation Ids
		DECLARE @tmpNewApplicationOperationId AS INT
		DECLARE @tmpHighApplicationOperationId AS INT

		--Count the number of rows in ApplicationOperation table
		select @Count=Count(*) from dbo.ApplicationOperation
		
		--this loop deletes old records from dbo.ApplicationOperation
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from temp IDTrack table
		SELECT @tmpNewApplicationOperationId = MAX(_NewID)
		FROM #tmp0IDTrack
		
		--Get old and High Number Ids for the selected New Id from IDTrack table
		SELECT @tmpApplicationOperationId =  _OldID, @tmpHighApplicationOperationId = _HighNumberID  FROM #tmp0IDTrack WHERE _NewID = @tmpNewApplicationOperationId
		
		--Delete old record from the original table in DB
		IF @tmpApplicationOperationId != @tmpNewApplicationOperationId AND @tmpApplicationOperationId > 0
		BEGIN
		DELETE FROM dbo.ApplicationOperation WHERE ApplicationOperationId = @tmpApplicationOperationId
		END

		-- Increment Loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp IDTrack table
		DELETE FROM #tmp0IDTrack WHERE _OldID = @tmpApplicationOperationId AND _NewID=@tmpNewApplicationOperationId

		SET ROWCOUNT 0
		END

		
		--make a copy of IDTrack table to iterate in loop
		Select * INTO #tmpIDTrack from #IDTrack
		
		--Set the count of number of rows in ApplicationOperation table
		select @Count=Count(*) from dbo.ApplicationOperation

		SET @iCNT = 1
		--This loop inserts existing records with new id values
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--select new ApplicationOperation id from idtrack table
		SELECT @tmpNewApplicationOperationId = MAX(_NewID)
		FROM #tmpIDTrack

		--select oldid from idtrack table
		SELECT @tmpApplicationOperationId =  _HighNumberID FROM #tmpIDTrack WHERE _NewID = @tmpNewApplicationOperationId
		--make a copy of existing record with new ID VALUE
		IF @tmpNewApplicationOperationId IS NOT NULL AND @tmpApplicationOperationId != @tmpNewApplicationOperationId AND NOT EXISTS (Select * from dbo.ApplicationOperation WHERE ApplicationOperationId = @tmpNewApplicationOperationID)
		BEGIN
		INSERT INTO dbo.ApplicationOperation (ApplicationOperationId, Name, Description, SortOrder, ApplicationId, SystemEntityTypeId, OperationValue)
		SELECT @tmpNewApplicationOperationId,  Name, Description, SortOrder, ApplicationId, SystemEntityTypeId, OperationValue
		FROM dbo.ApplicationOperation		
		WHERE ApplicationOperationId = @tmpApplicationOperationId    
		END   
		
		SET @iCNT = @iCNT + 1
		
		--DELETE processed id record from temp tab;le
		DELETE FROM #tmpIDTrack WHERE _HighNumberID = @tmpApplicationOperationId AND _NewID=@tmpNewApplicationOperationId

		SET ROWCOUNT 0

		END

		
		DROP TABLE #tmpIDTrack

		--Copy IDTrack contents into temp table
		Select * INTO #tmp2IDTrack from #IDTrack

		--Set counter to number of rows in temp table of IDTrack
		Select @Count = COUNT(*) from #tmp2IDTrack
		SET @iCNT = 1
		--This loop updates FK Tables to refer to new ID records of ApplicationOperation
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT
		--select highnumber id from temp table of idtrack
		DECLARE @tmpHighId AS INT
		SELECT @tmpHighId = MAX(_HighNumberId)
		FROM #tmp2IDTrack

		--select new id from temp table of idtrack
		SELECT @tmpNewApplicationOperationId =  _NewID FROM #tmp2IDTrack WHERE _HighNumberID = @tmpHighId
		
		--Update Project table with new ApplicationOperationid in the place of highnumber ApplicationOperationid
		UPDATE dbo.ApplicationOperationXApplicationRole
		SET ApplicationOperationId = @tmpNewApplicationOperationId	
		WHERE ApplicationOperationId = @tmpHighId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1
		
		--delete processed id record from temp table of idtrack
		DELETE FROM #tmp2IDTrack WHERE _NewID = @tmpNewApplicationOperationId AND _HighNumberID=@tmpHighId

		SET ROWCOUNT 0

		END

		Select * INTO #tmp3IDTrack from #IDTrack
		SET @iCNT = 1
		
		--Set counter to number of rows in ApplicationOperation table
		select @Count=Count(*) from dbo.ApplicationOperation
		
		--Select * from dbo.ApplicationOperation
		--this loop deleteshighnumbered records from dbo.ApplicationOperation
		WHILE @iCNT <= @Count
		BEGIN
    
		SET ROWCOUNT @iCNT

		--Get NewId from IDTrack table
		SELECT @tmpNewApplicationOperationId = MAX(_NewID)
		FROM #tmp3IDTrack

		--Get old and high number Ids for the selected New Id value
		SELECT @tmpApplicationOperationId =  _OldID, @tmpHighApplicationOperationId = _HighNumberID  FROM #tmp3IDTrack WHERE _NewID = @tmpNewApplicationOperationId
		
		--Delete High numbered Record from original table
		DELETE FROM dbo.ApplicationOperation WHERE ApplicationOperationId = @tmpHighApplicationOperationId   
		
		--Increment loop counter
		SET @iCNT = @iCNT + 1

		--Delete processed record from temp table of IDTrack
		DELETE FROM #tmp3IDTrack WHERE _OldID = @tmpApplicationOperationId AND _NewID=@tmpNewApplicationOperationId

		SET ROWCOUNT 0
		END

		--Update status in EntityRenumberStatus table
		UPDATE CommonServices.dbo.EntityRenumberStatus
		SET IsUnderRenumbering='NO' WHERE EntityName='ApplicationOperation'

		--Create back the Constraint once renumbering is done
		PRINT 'Creating Constraint'
		ALTER TABLE dbo.ApplicationOperation
		ADD CONSTRAINT UQ_ApplicationOperation_ApplicationId_Name UNIQUE NONCLUSTERED
		(
				ApplicationId
			,	Name	
		)

		END
		END
		GO


