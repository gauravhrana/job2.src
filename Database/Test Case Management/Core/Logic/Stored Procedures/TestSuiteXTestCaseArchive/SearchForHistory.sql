IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND Name = 'TestSuiteXTestCaseArchiveSearchHistory')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseArchiveSearchHistory'
	DROP Procedure TestSuiteXTestCaseArchiveSearchHistory
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseArchiveSearchHistory'
GO

/******************************************************************************
**		File: 
**		TestSuiteId: TestSuiteXTestCaseArchiveSearchHistory
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by: 

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				AssignedTo:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
Create procedure TestSuiteXTestCaseArchiveSearchHistory
(
		@TestSuiteXTestCaseArchiveId			INT				= NULL	
	,	@ApplicationId							INT				= NULL	
	,	@TestSuite								VARCHAR(50)		= NULL
	,	@TestCase								VARCHAR(50)		= NULL
	,	@TestCaseStatus							VARCHAR(50)		= NULL	
	,	@TestCasePriority						VARCHAR(50)		= NULL	
	,	@TestSuiteXTestCaseId					INT				= NULL	
	,	@AuditId								INT						
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntity							VARCHAR(50)		= 'TestSuiteXTestCaseArchive' 
)
AS
BEGIN

	-- assume search on all possiblities ('%')
	SET	@TestSuite		= ISNULL(@TestSuite,'%')
	SET	@TestCase		= ISNULL(@TestCase,'%')
	SET	@TestCaseStatus	= ISNULL(@TestCaseStatus,'%')
	SET	@TestCasePriority	= ISNULL(@TestCasePriority,'%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@TestSuite))) = 0 
	BEGIN
		SET	@TestSuite = '%'
	END

	IF LEN(LTRIM(RTRIM(@TestCase))) = 0 
	BEGIN
		SET	@TestCase = '%'
	END

	IF LEN(LTRIM(RTRIM(@TestCaseStatus))) = 0 
	BEGIN
		SET	@TestCaseStatus = '%'
	END

	IF LEN(LTRIM(RTRIM(@TestCasePriority))) = 0 
	BEGIN
		SET	@TestCasePriority = '%'
	END
		
	SELECT	a.TestSuiteXTestCaseArchiveId	
	,	a.ApplicationId
	,	a.TestSuiteId				
	,	a.TestCaseId					
	,	a.TestCaseStatusId				
	,	a.TestCasePriorityId
	,	a.RecordDate						
	,	a.TestSuite				
	,	a.TestCase					
	,	a.TestCaseStatus				
	,	a.TestCasePriority			
	,	a.TestSuiteXTestCaseId		
						
	,	a.KnowledgeDate					
	
	,	0					AS	'IsTestCaseStatusChanged'
	,	0					AS	'IsTestCasePriorityChanged'
		
	INTO	#TempTSTCA
	FROM	dbo.TestSuiteXTestCaseArchive a
	WHERE	a.ApplicationId							= ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.TestSuite						LIKE @TestSuite	+ '%'
	AND		a.TestCase							LIKE @TestCase	+ '%'
	AND		a.TestCaseStatus					LIKE @TestCaseStatus	+ '%'
	AND		a.TestCasePriority					LIKE @TestCasePriority	+ '%'
	AND		a.TestSuiteXTestCaseId			= ISNULL(@TestSuiteXTestCaseId, a.TestSuiteXTestCaseId)
	AND		a.TestSuiteXTestCaseArchiveId	= ISNULL(@TestSuiteXTestCaseArchiveId, a.TestSuiteXTestCaseArchiveId )
	ORDER BY a.RecordDate							ASC
		,	 a.TestSuiteXTestCaseArchiveId	ASC

	DECLARE @TempTS		AS 	VARCHAR(50)		=	NULL
	DECLARE	@TempTP		AS 	VARCHAR(50)		=	NULL
	
	DECLARE @MyTS		AS 	VARCHAR(50)
	DECLARE	@MyTP		AS 	VARCHAR(50)
	
	DECLARE @MyTSTCAId	AS	INT

	DECLARE db_cursor CURSOR FOR
	SELECT 	a.TestSuiteXTestCaseArchiveId	
		,	a.TestCaseStatus				
		,	a.TestCasePriority				
									
	FROM	dbo.TestSuiteXTestCaseArchive a
	WHERE	a.ApplicationId							= ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.TestSuite						LIKE @TestSuite	+ '%'
	AND		a.TestCase							LIKE @TestCase	+ '%'
	AND		a.TestCaseStatus					LIKE @TestCaseStatus	+ '%'
	AND		a.TestCasePriority					LIKE @TestCasePriority	+ '%'
	AND		a.TestSuiteXTestCaseId			= ISNULL(@TestSuiteXTestCaseId, a.TestSuiteXTestCaseId)
	AND		a.TestSuiteXTestCaseArchiveId	= ISNULL(@TestSuiteXTestCaseArchiveId, a.TestSuiteXTestCaseArchiveId )		
	ORDER BY	a.RecordDate
	
	OPEN db_cursor
	FETCH NEXT
	FROM db_cursor INTO @MyTSTCAId, @MyTS, @MyTP

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		IF	@TempTS IS NOT NULL AND @TempTP IS NOT NULL 
		BEGIN

			DECLARE @IsTSChanged	AS	INT = 0
			DECLARE @IsTPChanged	AS	INT = 0
			
			
			IF	@TempTS <> @MyTS
			BEGIN	
				SET @IsTSChanged = 1
			END
			
			IF	@TempTP <> @MyTP
			BEGIN	
				SET @IsTPChanged = 1
			END
			
			
			
			UPDATE	#TempTSTCA
			SET		IsTestCaseStatusChanged		=	@IsTSChanged
				,	IsTestCasePriorityChanged	=	@IsTPChanged
				
			WHERE	TestSuiteXTestCaseArchiveId	=	@MyTSTCAId			
		
		END
		
		SET	@TempTS 	= @MyTS
		SET	@TempTP 	= @MyTP
				
		
		FETCH NEXT
		FROM db_cursor INTO @MyTSTCAId, @MyTS, @MyTP

	END 

	CLOSE db_cursor 
	DEALLOCATE db_cursor

	SELECT	a.TestSuiteXTestCaseArchiveId	
		,	a.ApplicationId
		,	a.TestSuiteId				
		,	a.TestCaseId
		,	a.TestCaseStatusId				
		,	a.TestCasePriorityId
		,	a.RecordDate						
		--,	a.TestSuite				
		--,	a.TestCase			
		,	a.TestSuiteXTestCaseId				
		,	a.KnowledgeDate							
		--,	a.TestCaseStatus				
		--,	a.TestCasePriority		
		
		,	a.IsTestCaseStatusChanged
		,	a.IsTestCasePriorityChanged
		
	FROM	#TempTSTCA	a


	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@TestSuite		= @SystemEntity
		,	@EntityKey				= @TestSuiteXTestCaseArchiveId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
	

