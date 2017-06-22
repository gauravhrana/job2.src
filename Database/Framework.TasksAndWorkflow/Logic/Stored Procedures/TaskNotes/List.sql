IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskNotesList')
BEGIN
	PRINT 'Dropping Procedure TaskNotesList'
	DROP  Procedure  dbo.TaskNotesList
END
GO

PRINT 'Creating Procedure TaskNotesList'
GO

/******************************************************************************
**		File: 
**		Name: TaskNotesList
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
**     ----------					   ---------
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

CREATE Procedure dbo.TaskNotesList
(
		@AuditId				INT			
	,	@ApplicationId			INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskNotes'
)
AS
BEGIN

	SELECT	TaskNotesId		
		,	ApplicationId 
		,	Name				 
		,	Description		 
		,	SortOrder
		
	FROM		dbo.TaskNotes
	WHERE		ApplicationId = @ApplicationId
	ORDER BY	SortOrder	            ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO