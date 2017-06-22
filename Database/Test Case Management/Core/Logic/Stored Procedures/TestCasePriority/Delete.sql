	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCasePriorityDelete')
BEGIN
	PRINT 'Dropping Procedure TestCasePriorityDelete'
	DROP  Procedure TestCasePriorityDelete
END
GO

PRINT 'Creating Procedure TestCasePriorityDelete'
GO
/******************************************************************************
**		File: 
**		Name: TestCasePriorityDelete
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
CREATE Procedure dbo.TestCasePriorityDelete
(
		@TestCasePriorityId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'TestCasePriority'	
)
AS
BEGIN

	DELETE	 dbo.TestCasePriority
	WHERE	 TestCasePriorityId = @TestCasePriorityId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestCasePriorityId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
