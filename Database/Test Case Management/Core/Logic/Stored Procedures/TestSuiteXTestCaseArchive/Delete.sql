IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'TestSuiteXTestCaseArchiveDelete')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseArchiveDelete'
	DROP  Procedure TestSuiteXTestCaseArchiveDelete
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseArchiveDelete'
GO
/******************************************************************************
**		File: 
**		SystemEntityTypeId: TestSuiteXTestCaseArchiveDelete
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
CREATE Procedure dbo.TestSuiteXTestCaseArchiveDelete
(
		@TestSuiteXTestCaseArchiveId 	INT		
	,	@AuditId								INT						
	,	@AuditDate								DATETIME		=	NULL		
	,	@SystemEntityType						VARCHAR(200)	=	'TestSuiteXTestCaseArchive'
)
AS
BEGIN

	DELETE	 dbo.TestSuiteXTestCaseArchive
	WHERE	 TestSuiteXTestCaseArchiveId = @TestSuiteXTestCaseArchiveId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestSuiteXTestCaseArchiveId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
