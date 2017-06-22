	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseOwnerDelete')
BEGIN
	PRINT 'Dropping Procedure TestCaseOwnerDelete'
	DROP  Procedure TestCaseOwnerDelete
END
GO

PRINT 'Creating Procedure TestCaseOwnerDelete'
GO
/******************************************************************************
**		File: 
**		Name: TestCaseOwnerDelete
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
CREATE Procedure dbo.TestCaseOwnerDelete
(
		@TestCaseOwnerId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'TestCaseOwner'	
)
AS
BEGIN

	DELETE	 dbo.TestCaseOwner
	WHERE	 TestCaseOwnerId = @TestCaseOwnerId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestCaseOwnerId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
