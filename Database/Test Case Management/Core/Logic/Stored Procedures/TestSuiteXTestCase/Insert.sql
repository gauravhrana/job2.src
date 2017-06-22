IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestSuiteXTestCaseInsert')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseInsert'
	DROP  Procedure TestSuiteXTestCaseInsert
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TestSuiteXTestCaseInsert
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.TestSuiteXTestCaseInsert
(
		@TestSuiteXTestCaseId			INT			= NULL 	OUTPUT		
	,	@TestSuiteId					INT								
	,	@TestCaseId					INT						
	,	@TestCaseStatusId			INT					
	,	@TestCasePriorityId		INT					
	,	@ApplicationId				INT		
	,	@AuditId					INT									
	,	@AuditDate					DATETIME	= NULL				
	,	@SystemEntityType			VARCHAR(50)	= 'TestSuiteXTestCase'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TestSuiteXTestCaseId OUTPUT, @AuditId
	
	INSERT INTO dbo.TestSuiteXTestCase 
	( 
			TestSuiteXTestCaseId						
		,	TestSuiteId				
		,	TestCaseId
		,	TestCaseStatusId
		,	TestCasePriorityId
		,	ApplicationId						
	)
	VALUES 
	(  
			@TestSuiteXTestCaseId					
		,	@TestSuiteId
		,	@TestCaseId	
		,	@TestCaseStatusId
		,	@TestCasePriorityId		
		,	@ApplicationId	
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestSuiteXTestCaseId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 