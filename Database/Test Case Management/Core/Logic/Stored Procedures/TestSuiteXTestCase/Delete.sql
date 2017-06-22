IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestSuiteXTestCaseDelete')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseDelete'
	DROP  Procedure TestSuiteXTestCaseDelete
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseDelete'
GO
/******************************************************************************
**		File: 
**		Name: TestSuiteXTestCaseDelete
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.TestSuiteXTestCaseDelete
(
		@TestSuiteXTestCaseId 		INT			= NULL		
	,	@TestSuiteId 				INT			= NULL		
	,	@TestCaseId 				INT			= NULL	
	,	@TestCaseStatusId				INT				= NULL	
	,	@TestCasePriorityId			INT				= NULL		
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestSuiteXTestCase'
)
AS
BEGIN

	DELETE	dbo.TestSuiteXTestCase
	WHERE	TestSuiteXTestCaseId	=	ISNULL(@TestSuiteXTestCaseId,	TestSuiteXTestCaseId)	
	AND		TestSuiteId			=	ISNULL(@TestSuiteId,			TestSuiteId)
	AND		TestCaseId			=	ISNULL(@TestCaseId,			TestCaseId)
	AND		TestCaseStatusId				= ISNULL(@TestCaseStatusId, TestCaseStatusId)
	AND		TestCasePriorityId			= ISNULL(@TestCasePriorityId, TestCasePriorityId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TestSuiteXTestCase'
		,	@EntityKey				= @TestSuiteXTestCaseId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
