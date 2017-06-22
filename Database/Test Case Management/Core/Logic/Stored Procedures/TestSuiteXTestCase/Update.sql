IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestSuiteXTestCaseUpdate')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseUpdate'
	DROP  Procedure  TestSuiteXTestCaseUpdate
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TestSuiteXTestCaseUpdate
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TestSuiteXTestCaseUpdate
(
		@TestSuiteXTestCaseId		INT		
		,	@ApplicationId					INT	 			
	,	@TestSuiteId				INT					
	,	@TestCaseId				INT			
	,	@AuditId				INT		
	,	@TestCaseStatusId			INT					
	,	@TestCasePriorityId		INT								
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'TestSuiteXTestCase'
)
AS
BEGIN 

	UPDATE	dbo.TestSuiteXTestCase 
	SET		TestSuiteId				=	@TestSuiteId			
		,	TestCaseId				=	@TestCaseId	
		,	TestCaseStatusId		=	@TestCaseStatusId				
		,	TestCasePriorityId		=	@TestCasePriorityId								
	WHERE	TestSuiteXTestCaseId		=	@TestSuiteXTestCaseId


	DECLARE @TestSuiteId		AS INT
	DECLARE @TestCaseId			AS INT
	DECLARE @TestCaseStatusId	AS INT
	DECLARE @TestCasePriorityId	AS INT
	DECLARE @TestSuite			AS VARCHAR(50)
	DECLARE @TestCase			AS VARCHAR(50)
	DECLARE @TestCaseStatus		AS VARCHAR(50)
	DECLARE @TestCasePriority	AS VARCHAR(50)
	DECLARE @AcknowledgedBy		AS VARCHAR(50)

	
	SELECT	@TestSuite = a.Name
	FROM	dbo.TestSuite	a
	WHERE	a.TestSuiteId = @TestSuiteId

	SELECT	@TestCase = a.Name
	FROM	dbo.TestCase	a
	WHERE	a.TestCaseId = @TestCaseId

	SELECT	@TestCaseStatus = a.Name
	FROM	dbo.TestCaseStatus	a
	WHERE	a.TestCaseStatusId = @TestCaseStatusId

	SELECT	@TestCasePriority = a.Name
	FROM	dbo.TestCasePriority	a
	WHERE	a.TestCasePriority = @TestCasePriority			

	DECLARE @RecordDate AS DECIMAL
	SELECT @RecordDate = CAST( CONVERT(VARCHAR(8), GETDATE(), 112) + REPLACE(CONVERT(varchar(5), GETDATE(), 114), ':', '') AS DECIMAL)		
	
	-- Create Archive Record
	EXEC	dbo.TestSuiteXTestCaseArchiveInsert						
		@ApplicationId					=	@ApplicationId	
	,	@RecordDate						=	@RecordDate
	,	@TestSuiteId					=	@TestSuiteId			
	,	@TestCaseId						=	@TestCaseId		
	,	@TestCaseStatusId				=	@TestCaseStatusId		
	,	@TestCasePriorityId				=	@TestCasePriorityId
	,	@TestSuite						=	@TestSuite			
	,	@TestCase						=	@TestCase		
	,	@TestCaseStatus					=	@TestCaseStatus		
	,	@TestCasePriority				=	@TestCasePriority
	,	@TestSuiteXTestCaseId			=	@TestSuiteXTestCaseId		
	,	@KnowledgeDate					=	@RecordDate	
	,	@AuditId						=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TestSuiteXTestCase'
		,	@EntityKey				= @TestSuiteXTestCaseId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO