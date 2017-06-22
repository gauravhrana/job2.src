IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityDelete')
BEGIN
	PRINT 'Dropping Procedure TaskEntityDelete'
	DROP  Procedure TaskEntityDelete
END
GO

PRINT 'Creating Procedure TaskEntityDelete'
GO
/******************************************************************************
**		File: 
**		Name: TaskEntityDelete
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
CREATE Procedure dbo.TaskEntityDelete
(
		@TaskEntityId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'TaskEntity'	
)
AS
BEGIN

	DELETE	 dbo.TaskEntity
	WHERE	 TaskEntityId = @TaskEntityId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskEntityId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
