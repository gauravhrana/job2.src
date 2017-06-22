IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskNotesDelete')
BEGIN
	PRINT 'Dropping Procedure TaskNotesDelete'
	DROP  Procedure TaskNotesDelete
END
GO

PRINT 'Creating Procedure TaskNotesDelete'
GO
/******************************************************************************
**		File: 
**		Name: TaskNotesDelete
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
CREATE Procedure dbo.TaskNotesDelete
(
		@TaskNotesId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'TaskNotes'	
)
AS
BEGIN

	DELETE	 dbo.TaskNotes
	WHERE	 TaskNotesId = @TaskNotesId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskNotesId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
