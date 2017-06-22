IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'TestSuiteXTestCaseArchiveClone')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseArchiveClone'
	DROP  Procedure TestSuiteXTestCaseArchiveClone
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseArchiveClone'
GO

/*********************************************************************************************
**		File: 
**		TestSuiteId: TestSuiteXTestCaseArchiveClone
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
**		Date:		Author:				AssignedTo:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.TestSuiteXTestCaseArchiveClone
(
		@TestSuiteXTestCaseArchiveId		INT				= NULL 	OUTPUT		
	,	@ApplicationId						INT			
	,	@RecordDate							DECIMAL(15,0)
	,	@TestSuiteId						INT		
	,	@TestCaseId							INT					
	,	@TestCaseStatusId					INT					
	,	@TestCasePriorityId					INT	
	,	@TestSuite							VARCHAR(50)		
	,	@TestCase							VARCHAR(50)					
	,	@TestCaseStatus						VARCHAR(50)					
	,	@TestCasePriority					VARCHAR(50)		
	,	@TestSuiteXTestCaseId				INT		
	
	,	@KnowledgeDate						DECIMAL(15,0)	
			
	,	@AuditId							INT									
	,	@AuditDate							DATETIME		= NULL
	,	@SystemEntity						VARCHAR(50)	= 'TestSuiteXTestCaseArchive'
)
AS
BEGIN		
	
	SELECT	@TestSuiteId			=	TestSuiteId			
		,	@TestCaseId				=	TestCaseId			
		,   @TestCaseStatusId		=	TestCaseStatusId			
		,	@TestCasePriorityId		=	TestCasePriorityId				
		,	@RecordDate				=	RecordDate	
		,	@TestSuite				=	TestSuite			
		,	@TestCase				=	TestCase			
		,   @TestCaseStatus			=	TestCaseStatus			
		,	@TestCasePriority		=	TestCasePriority		
		,	@TestSuiteXTestCaseId	=	TestSuiteXTestCaseId	
		
		,   @KnowledgeDate			=   KnowledgeDate				
		
	FROM	dbo.TestSuiteXTestCaseArchive
	WHERE   TestSuiteXTestCaseArchiveId	= @TestSuiteXTestCaseArchiveId

	EXEC dbo.TestSuiteXTestCaseArchiveInsert 
			@TestSuiteXTestCaseArchiveId	=	NULL
		,	@ApplicationId						=	@ApplicationId
		,	@RecordDate							=	@RecordDate					
		,	@TestSuite					=	@TestSuite			
		,	@TestCase						=	@TestCase				
		,   @TestCaseStatus				=	@TestCaseStatus			
		,	@TestCasePriority				=	@TestCasePriority		
		,	@TestSuiteXTestCaseId		=	@TestSuiteXTestCaseId		
		,   @KnowledgeDate						=   @KnowledgeDate		
		
		,	@AuditId							=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntity
		,	@EntityKey				= @TestSuiteXTestCaseArchiveId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
