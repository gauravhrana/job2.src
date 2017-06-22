IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityClone')
BEGIN
	PRINT 'Dropping Procedure TaskEntityClone'
	DROP  Procedure TaskEntityClone
END 
GO

PRINT 'Creating Procedure TaskEntityClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TaskEntityClone
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

CREATE Procedure dbo.TaskEntityClone
(
		@TaskEntityId			INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT			= NULL	
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

	SELECT	@ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		,	@TaskEntityTypeId	= ISNULL(@TaskEntityTypeId, TaskEntityTypeId)
		,	@Description		= ISNULL(@Description, Description)
		,	@Active				= ISNULL(@Active, Active)
		,	@SortOrder			= ISNULL(@SortOrder, SortOrder)				
	FROM	dbo.TaskEntity
	WHERE	TaskEntityId		= @TaskEntityId

	EXEC dbo.TaskEntityInsert 
			@TaskEntityId		=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@TaskEntityTypeId	=	@TaskEntityTypeId
		,	@Description		=	@Description
		,	@Active				=	@Active
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskEntityId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
