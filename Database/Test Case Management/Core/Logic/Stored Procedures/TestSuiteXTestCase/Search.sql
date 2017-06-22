	IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='TestSuiteXTestCaseSearch')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseSearch'
	DROP Procedure TestSuiteXTestCaseSearch
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseSearch'
GO

/******************************************************************************
**		File: 
**		Name: TestSuiteXTestCaseSearch
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
			EXEC TestSuiteXTestCaseSearch NULL	, NULL	, NULL
			EXEC TestSuiteXTestCaseSearch NULL	, 'K'	, NULL
			EXEC TestSuiteXTestCaseSearch 1		, 'K'	, NULL
			EXEC TestSuiteXTestCaseSearch 1		, NULL	, NULL
			EXEC TestSuiteXTestCaseSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.TestSuiteXTestCaseSearch
(
		@TestSuiteXTestCaseId		INT				= NULL 	
	,	@TestSuiteId				INT				= NULL 	
	,	@TestCaseId					INT				= NULL 
	,	@ApplicationId				INT				= NULL 	
	,	@TestCaseStatusList			VARCHAR(200)				= NULL	
	,	@TestCasePriorityId			INT				= NULL	
	,	@TestCaseStatusId			INT				= NULL	
	,	@AuditId					INT							
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'TestSuiteXTestCase'	
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				= 1
	,	@AddTraceInfo			INT				= 0
	,	@ReturnAuditInfo		INT				= 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON

	SELECT	a.TestSuiteXTestCaseId												
		,	a.TestSuiteId																
		,	a.TestCaseId
		,	a.TestCaseStatusId		
		,	a.TestCasePriorityId
		,	d.Name							AS 'TestCasePriority'	
		,	e.Name							AS 'TestCaseStatus'
		,	b.Name					AS 'TestSuite'
		,	c.Name					AS 'TestCase'
	FROM		dbo.TestSuiteXTestCase	a
	INNER JOIN	dbo.TestSuite			b ON a.TestSuiteId					= b.TestSuiteId
	INNER JOIN	dbo.TestCase			c ON a.TestCaseId				= c.TestCaseId
	INNER JOIN	dbo.TestCasePriority			d ON a.TestCasePriorityId				= d.TestCasePriorityId
	INNER JOIN	dbo.TestCaseStatus			e ON a.TestCaseStatusId				= e.TestCaseStatusId

	WHERE	a.TestSuiteId			= ISNULL(@TestSuiteId, a.TestSuiteId)
	AND		a.TestCaseId			= ISNULL(@TestCaseId, a.TestCaseId)
	AND		a.TestCaseStatusId		= ISNULL(@TestCaseStatusId, a.TestCaseStatusId)
	AND		a.TestSuiteXTestCaseId	= ISNULL(@TestSuiteXTestCaseId, a.TestSuiteXTestCaseId)  	  
	AND 
	(
    (@TestCaseStatusList IS NOT NULL AND  CAST(a.TestCaseStatusId AS VARCHAR(50)) IN (SELECT * FROM dbo.Split(@TestCaseStatusList) ))
      OR
    (@TestCaseStatusList IS NULL AND a.TestCaseStatusId LIKE '%%')
)
	
	
	--SELECT * FROM FieldConfiguration WHERE FieldConfigurationModeId=10019	
	--AND		a.TestCasePriorityId	= ISNULL(@TestCasePriorityId, a.TestCasePriorityId)
	--AND		a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId)

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'TestSuiteXTestCase'
				,	@EntityKey				= @TestSuiteXTestCaseId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId

		END

END
GO

