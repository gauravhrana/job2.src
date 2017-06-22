IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'TestSuiteXTestCaseArchiveInsert')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseArchiveInsert'
	DROP  Procedure TestSuiteXTestCaseArchiveInsert
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseArchiveInsert'
GO

/*********************************************************************************************
**		File: 
**		SystemEntityTypeId:TestSuiteXTestCaseArchiveInsert
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
**********************************************************************************************/

CREATE Procedure dbo.TestSuiteXTestCaseArchiveInsert
(
		@TestSuiteXTestCaseArchiveId			INT				= NULL 	OUTPUT						
	,	@ApplicationId							INT			
	,	@RecordDate								DECIMAL(15,0)
	,	@TestSuiteId							INT		
	,	@TestCaseId								INT					
	,	@TestCaseStatusId						INT				
	,	@TestCasePriorityId						INT		
	,	@TestSuite								VARCHAR(50)		
	,	@TestCase								VARCHAR(50)					
	,	@TestCaseStatus							VARCHAR(50)					
	,	@TestCasePriority						VARCHAR(50)		
	,	@TestSuiteXTestCaseId					INT				
	
	,	@KnowledgeDate							DECIMAL(15,0)	
	
	,	@AuditId								INT									
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntity							VARCHAR(50)		= 'TestSuiteXTestCaseArchive'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntity, @TestSuiteXTestCaseArchiveId OUTPUT, @AuditId

	INSERT INTO dbo.TestSuiteXTestCaseArchive 
	( 
			ApplicationId							
		,	RecordDate	
		,	TestSuiteId
		,	TestCaseId							
		,	TestCaseStatusId				
		,	TestCasePriorityId					
		,	TestSuite
		,	TestCase							
		,	TestCaseStatus				
		,	TestCasePriority				
		,	TestSuiteXTestCaseId		
								
		,	KnowledgeDate						
		
	)
	VALUES 
	(  
			@ApplicationId				
		,	@RecordDate	
		,	@TestSuiteId
		,	@TestCaseId				
		,	@TestCaseStatusId		
		,	@TestCasePriorityId				
		,	@TestSuite
		,	@TestCase				
		,	@TestCaseStatus			
		,	@TestCasePriority		
		,	@TestSuiteXTestCaseId	
						
		,	@KnowledgeDate				
			)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntity
		,	@EntityKey				= @TestSuiteXTestCaseArchiveId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 