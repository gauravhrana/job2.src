IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskNotesUpdate')
BEGIN
	PRINT 'Dropping Procedure TaskNotesUpdate'
	DROP  Procedure  TaskNotesUpdate
END
GO

PRINT 'Creating Procedure TaskNotesUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TaskNotesUpdate
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

CREATE Procedure dbo.TaskNotesUpdate
(
		@TaskNotesId			INT		
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(100)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'TaskNotes'
)
AS
BEGIN 

	UPDATE	dbo.TaskNotes 
	SET		Name				=	@Name				
		,	Description			=	@Description			
		,	SortOrder			=	@SortOrder							
	WHERE	TaskNotesId			=	@TaskNotesId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TaskNotesId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO