IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityUpdate')
BEGIN
	PRINT 'Dropping Procedure TaskEntityUpdate'
	DROP  Procedure  TaskEntityUpdate
END
GO

PRINT 'Creating Procedure TaskEntityUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TaskEntityUpdate
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

CREATE Procedure dbo.TaskEntityUpdate
(
		@TaskEntityId			INT		
	,	@Name					VARCHAR(50)				
	,	@TaskEntityTypeId		INT					
	,	@Description			VARCHAR(50)			
	,	@Active					INT					
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'TaskEntity'
)
AS
BEGIN 

	UPDATE	dbo.TaskEntity 
	SET		Name				=	@Name				
		,	TaskEntityTypeId	=	@TaskEntityTypeId	
		,	Description			=	@Description			
		,	Active				=	@Active						
		,	SortOrder			=	@SortOrder							
	WHERE	TaskEntityId		=	@TaskEntityId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TaskEntityId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO