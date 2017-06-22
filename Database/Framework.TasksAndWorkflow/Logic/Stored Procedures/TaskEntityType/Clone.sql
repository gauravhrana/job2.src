IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityTypeClone')
BEGIN
	PRINT 'Dropping Procedure TaskEntityTypeClone'
	DROP  Procedure TaskEntityTypeClone
END
GO

PRINT 'Creating Procedure TaskEntityTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TaskEntityTypeClone
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

CREATE Procedure dbo.TaskEntityTypeClone
(
		@TaskEntityTypeId		INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT			= NULL

	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@Active					INT		
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskEntityType'				
)
AS
BEGIN		

	SELECT	@ApplicationId  =	ISNULL(@ApplicationId, ApplicationId)
		,	@Description	=	ISNULL(@Description, Description)
		,	@SortOrder		=	ISNULL(@SortOrder, SortOrder)	
		,	@SortOrder		=	SortOrder				
	FROM	dbo.TaskEntityType
	WHERE	TaskEntityTypeId		= @TaskEntityTypeId

	EXEC dbo.TaskEntityTypeInsert 
			@TaskEntityTypeId	=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@Active				=	@Active
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskEntityTypeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
