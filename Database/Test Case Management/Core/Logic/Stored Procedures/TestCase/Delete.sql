	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseDelete')
BEGIN
	PRINT 'Dropping Procedure TestCaseDelete'
	DROP  Procedure TestCaseDelete
END
GO

PRINT 'Creating Procedure TestCaseDelete'
GO
/******************************************************************************
**		File: 
**		Name: TestCaseDelete
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
CREATE Procedure dbo.TestCaseDelete
(
		@TestCaseId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'TestCase'	
)
AS
BEGIN

	DELETE	 dbo.TestCase
	WHERE	 TestCaseId = @TestCaseId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestCaseId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
