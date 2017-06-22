IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleTypeClone')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleTypeClone'
	DROP  Procedure TaskScheduleTypeClone
END
GO

PRINT 'Creating Procedure TaskScheduleTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TaskScheduleTypeClone
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

CREATE Procedure dbo.TaskScheduleTypeClone
(
		@TaskScheduleTypeId		INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT			= NULL	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@Active					INT								
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskScheduleType'
)
AS
BEGIN		
	
	SELECT	@ApplicationId	=	ISNULL(@ApplicationId, ApplicationId)
		,	@Description	=	ISNULL(@Description, Description)
		,	@SortOrder		=	ISNULL(@SortOrder, SortOrder)	
		,	@SortOrder			= SortOrder				
	FROM	dbo.TaskScheduleType
	WHERE	TaskScheduleTypeId		= @TaskScheduleTypeId

	EXEC dbo.TaskScheduleTypeInsert 
			@TaskScheduleTypeId	=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@Active				=	@Active
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleTypeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
