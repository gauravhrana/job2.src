IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestSuiteXTestCaseClone')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseClone'
	DROP  Procedure TestSuiteXTestCaseClone
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TestSuiteXTestCaseClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/
CREATE Procedure dbo.TestSuiteXTestCaseClone
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

	SELECT	@TestSuiteId			= TestSuiteId			
		,	@TestCaseId			= TestCaseId	
		,	@TestCaseStatusId		= TestCaseStatusId	
		,   @TestCasePriorityId	= TestCasePriorityId
		,	@TestSuiteId 	=	TestSuiteId	
	,	@TestCaseId 			=	TestCaseId
		


	FROM	dbo.TestSuiteXTestCase
	WHERE   TestSuiteXTestCaseId	= @TestSuiteXTestCaseId
	ORDER BY TestSuiteXTestCaseId

	EXEC dbo.TestSuiteXTestCaseInsert 
			@TestSuiteXTestCase			=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@TestCaseId				=	@TestCaseId
		,	@TestSuiteId=@TestSuiteId
			,@TestCaseStatusId=@TestCaseStatusId
		,	@TestCasePriorityId=@TestCasePriorityId
		,	@AuditId				=	@AuditId

	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TestSuiteXTestCase'
		,	@EntityKey				= @TestSuiteXTestCaseId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

	

