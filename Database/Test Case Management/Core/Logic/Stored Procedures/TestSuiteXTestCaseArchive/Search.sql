IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND Name = 'TestSuiteXTestCaseArchiveSearch')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseArchiveSearch'
	DROP Procedure TestSuiteXTestCaseArchiveSearch
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseArchiveSearch'
GO

/******************************************************************************
**		File: 
**		TestSuiteId: TestSuiteXTestCaseArchiveSearch
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
			EXEC TestSuiteXTestCaseArchiveSearch NULL	, NULL	, NULL
			EXEC TestSuiteXTestCaseArchiveSearch NULL	, 'K'	, NULL
			EXEC TestSuiteXTestCaseArchiveSearch 1		, 'K'	, NULL
			EXEC TestSuiteXTestCaseArchiveSearch 1		, NULL	, NULL
			EXEC TestSuiteXTestCaseArchiveSearch NULL	, NULL	, 'W'

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
Create procedure TestSuiteXTestCaseArchiveSearch
(
		@TestSuiteXTestCaseArchiveId		INT				= NULL	
	,	@ApplicationId						INT				= NULL	
	,	@TestSuite							VARCHAR(50)		= NULL
	,	@TestCase							VARCHAR(50)		= NULL
	,	@TestCaseStatus						VARCHAR(50)		= NULL	
	,	@TestCasePriority					VARCHAR(50)		= NULL	
	,	@TestSuiteXTestCaseId				INT				= NULL	
	,	@AuditId							INT						
	,	@AuditDate							DATETIME		= NULL
	,	@SystemEntity						VARCHAR(50)		= 'TestSuiteXTestCaseArchive' 
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				= 1
	,	@AddTraceInfo						INT				= 0
	,	@ReturnAuditInfo					INT				= 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON

	-- assume search on all possiblities ('%')
	SET	@TestSuite = ISNULL(@TestSuite,'%')
	SET	@TestCase = ISNULL(@TestCase,'%')
	SET	@TestCaseStatus = ISNULL(@TestCaseStatus,'%')
	SET	@TestCasePriority = ISNULL(@TestCasePriority,'%')

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
    
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityId AS INT
	Select @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntity)	
		
	SELECT	a.TestSuiteXTestCaseArchiveId	
		,	a.ApplicationId
		,	a.RecordDate						
		,	a.TestSuiteId				
		,	a.TestCaseId					
		,	a.TestCaseStatusId				
		,	a.TestCasePriorityId
		,	a.TestSuite				
		,	a.TestCase					
		,	a.TestCaseStatus				
		,	a.TestCasePriority		
		,	a.TestSuiteXTestCaseId	
		,	a.KnowledgeDate			
	INTO	#TempMain			
	FROM	dbo.TestSuiteXTestCaseArchive			a	
	WHERE	a.ApplicationId							= ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.TestSuite						LIKE @TestSuite	+ '%'
	AND		a.TestCase							LIKE @TestCase	+ '%'
	AND		a.TestCaseStatus					LIKE @TestCaseStatus	+ '%'
	AND		a.TestCasePriority					LIKE @TestCasePriority	+ '%'
	AND		a.TestSuiteXTestCaseId			= ISNULL(@TestSuiteXTestCaseId, a.TestSuiteXTestCaseId)
	AND		a.TestSuiteXTestCaseArchiveId	= ISNULL(@TestSuiteXTestCaseArchiveId, a.TestSuiteXTestCaseArchiveId )
	ORDER BY a.TestSuiteXTestCaseId	ASC
		,	 a.TestSuiteXTestCaseArchiveId	ASC
		
	IF @ReturnAuditInfo = 1
		BEGIN
	
			-- get Audit latest record matching on key, TestSuite
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.TestSuiteXTestCaseArchiveId
						AND c.SystemEntityId	= @SystemEntityId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.TestSuiteXTestCaseArchiveId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.TestSuiteXTestCaseArchiveId
			INNER JOIN	CommonServices.dbo.AuditHistory						c
						ON	c.AuditHistoryId	= b.MaxAuditHistoryId
			INNER JOIN	CommonServices.dbo.AuditAction						d	
						ON	c.AuditActionId 	= d.AuditActionId
			INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
						ON	c.CreatedByPersonId	= e.ApplicationUserId		
	
			SELECT 	a.*					
				, 	b.UpdatedDate
				,	b.UpdatedBy
				,	b.LastAction
			FROM #TempMain a
			LEFT JOIN #HistortyInfoDetails	b	
						ON	a.TestSuiteXTestCaseArchiveId	= b.TestSuiteXTestCaseArchiveId
			ORDER BY	a.TestSuiteXTestCaseArchiveId
		
		END
	ELSE
		BEGIN
		
			SELECT 	a.*
				, 	UpdatedDate = '1/1/1900'
				,	UpdatedBy	= 'Unknown'
				,	LastAction	= 'Unknown'
			FROM	#TempMain a		
			ORDER BY	a.TestSuiteXTestCaseArchiveId
					
		END

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntity
				,	@EntityKey				= @TestSuiteXTestCaseArchiveId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END
GO
	

