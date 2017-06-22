IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND Name = 'TestSuiteXTestCaseArchiveDoesExist')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseArchiveDoesExist'
	DROP  Procedure  TestSuiteXTestCaseArchiveDoesExist
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseArchiveDoesExist'
GO

/******************************************************************************
**		Task: 
**		TestSuiteId: TestSuiteXTestCaseArchiveDoesExist
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
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

Create procedure dbo.TestSuiteXTestCaseArchiveDoesExist
(
		@TestSuiteXTestCaseArchiveId		INT		 
	,	@TestSuiteId						INT		
	,	@TestCaseId							INT					
	,	@TestCaseStatusId					INT					
	,	@TestCasePriorityId					INT	
	,	@TestSuite							VARCHAR(50)		
	,	@TestCase							VARCHAR(50)					
	,	@TestCaseStatus						VARCHAR(50)					
	,	@TestCasePriority					VARCHAR(50)		
	,	@TestSuiteXTestCaseId				INT	
	,	@ApplicationId						INT	
	,	@AuditId							INT							
	,	@AuditDate							DATETIME		= NULL		
	,	@SystemEntity						VARCHAR(200)	= 'TestSuiteXTestCaseArchive'	 		
)
AS
BEGIN	

	SELECT	a.*
	FROM	dbo.TestSuiteXTestCaseArchive a
	WHERE	a.TestSuiteId				=	@TestSuiteId			
	AND		a.TestCaseId				=	@TestCase				
	AND		a.TestCaseStatusId			=	@TestCaseStatus		
	AND		a.TestCasePriorityId		=	@TestCasePriority
	AND		a.TestSuite					=	@TestSuite			
	AND		a.TestCase					=	@TestCase				
	AND		a.TestCaseStatus			=	@TestCaseStatus		
	AND		a.TestCasePriority			=	@TestCasePriority		
	AND		a.TestSuiteXTestCaseId		=	@TestSuiteXTestCaseId
	AND		a.ApplicationId				=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntity
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

