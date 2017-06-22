IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'TestSuiteXTestCaseArchiveUpdate')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseArchiveUpdate'
	DROP  Procedure  TestSuiteXTestCaseArchiveUpdate
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseArchiveUpdate'
GO

/******************************************************************************
**		File: 
**		TestSuiteId: TestSuiteXTestCaseArchiveUpdate
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
**		Date:		Author:				AssignedTo:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TestSuiteXTestCaseArchiveUpdate
(
		@TestSuiteXTestCaseArchiveId		INT			
	,	@RecordDate								DECIMAL(15,0)
	,	@TestSuiteId				INT					
	,	@TestCaseId				INT			
	,	@TestCaseStatusId			INT					
	,	@TestCasePriorityId		INT				
	,	@TestSuite						VARCHAR(50)		
	,	@TestCase							VARCHAR(50)					
	,	@TestCaseStatus					VARCHAR(50)					
	,	@TestCasePriority					VARCHAR(50)		
	,	@TestSuiteXTestCaseId			INT		
	,	@KnowledgeDate							DECIMAL(15,0)		
	,	@AuditId								INT									
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntity							VARCHAR(50)	= 'TestSuiteXTestCaseArchive'
)
AS
BEGIN 

	--DECLARE @Date AS DECIMAL
	--SELECT @Date = CAST( CONVERT(VARCHAR(8), GETDATE(), 112) + REPLACE(CONVERT(varchar(5), GETDATE(), 114), ':', '') AS DECIMAL)

	UPDATE	dbo.TestSuiteXTestCaseArchive 
	SET		RecordDate							=	@RecordDate	
		,	TestSuiteId						=	@TestSuiteId
		,	TestCaseId						=	@TestCaseId
		,	TestCaseStatusId				=	@TestCaseStatusId
		,	TestCasePriorityId				=	@TestCasePriorityId						
		,	TestSuite						=	@TestSuite					
		,	TestCase						=	@TestCase							
		,	TestCaseStatus					=	@TestCaseStatus						
		,	TestCasePriority				=	@TestCasePriority				
		,	TestSuiteXTestCaseId			=	@TestSuiteXTestCaseId
		
		,	KnowledgeDate						=	@KnowledgeDate						
		
	WHERE	TestSuiteXTestCaseArchiveId	=	@TestSuiteXTestCaseArchiveId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntity	
		,	@EntityKey				= @TestSuiteXTestCaseArchiveId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO