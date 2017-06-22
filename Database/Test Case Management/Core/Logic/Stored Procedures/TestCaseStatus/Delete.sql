	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseStatusDelete')
BEGIN
	PRINT 'Dropping Procedure TestCaseStatusDelete'
	DROP  Procedure TestCaseStatusDelete
END
GO

PRINT 'Creating Procedure TestCaseStatusDelete'
GO
/******************************************************************************
**		File: 
**		Name: TestCaseStatusDelete
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
CREATE Procedure dbo.TestCaseStatusDelete
(
		@TestCaseStatusId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'TestCaseStatus'	
)
AS
BEGIN

	DELETE	 dbo.TestCaseStatus
	WHERE	 TestCaseStatusId = @TestCaseStatusId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestCaseStatusId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
