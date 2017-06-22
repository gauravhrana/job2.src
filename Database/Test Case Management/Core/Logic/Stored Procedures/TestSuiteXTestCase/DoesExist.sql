IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TestSuiteXTestCaseDoesExist')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseDoesExist'
	DROP  Procedure  TestSuiteXTestCaseDoesExist
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TestSuiteXTestCaseDoesExist
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
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.TestSuiteXTestCaseDoesExist
(
		@TestSuiteXTestCaseId		INT				= NULL		
	,	@TestSuiteId				INT							
	,	@TestCaseId				INT	
	,	@ApplicationId			INT							
	,	@TestCaseStatusId			INT					
	,	@TestCasePriorityId		INT		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'TestSuiteXTestCase'			
)
AS
BEGIN	

	SELECT	a.*
	FROM	dbo.TestSuiteXTestCase a
	WHERE	a.TestSuiteId		=	@TestSuiteId	
	AND		a.TestCaseId		=	@TestCaseId	
	AND		a.TestCaseStatusId	=	@TestCaseStatusId
	AND		a.TestCasePriorityId	=	@TestCasePriorityId
	
	AND		a.ApplicationId	=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TestSuiteXTestCase'
		,	@EntityKey				= @TestSuiteXTestCaseId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

