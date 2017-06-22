IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityTypeDelete')
BEGIN
	PRINT 'Dropping Procedure TaskEntityTypeDelete'
	DROP  Procedure TaskEntityTypeDelete
END
GO

PRINT 'Creating Procedure TaskEntityTypeDelete'
GO
/******************************************************************************
**		File: 
**		Name: TaskEntityTypeDelete
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
CREATE Procedure dbo.TaskEntityTypeDelete
(
		@TaskEntityTypeId 		INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskEntityType'
)
AS
BEGIN

	DELETE	 dbo.TaskEntityType
	WHERE	 TaskEntityTypeId = @TaskEntityTypeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskEntityTypeId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
