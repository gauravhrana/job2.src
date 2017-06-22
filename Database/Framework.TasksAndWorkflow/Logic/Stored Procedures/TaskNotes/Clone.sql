IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskNotesClone')
BEGIN
	PRINT 'Dropping Procedure TaskNotesClone'
	DROP  Procedure TaskNotesClone
END 
GO

PRINT 'Creating Procedure TaskNotesClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TaskNotesClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.TaskNotesClone
(
		@TaskNotesId			INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT			= NULL	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(100)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'TaskNotes'			
)
AS
BEGIN		

	SELECT	@ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		,	@Description		= ISNULL(@Description, Description)
		,	@SortOrder			= ISNULL(@SortOrder, SortOrder)				
	FROM	dbo.TaskNotes
	WHERE	TaskNotesId		= @TaskNotesId

	EXEC dbo.TaskNotesInsert 
			@TaskNotesId		=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskNotesId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
