IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleClone')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleClone'
	DROP  Procedure TaskScheduleClone
END
GO

PRINT 'Creating Procedure TaskScheduleClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TaskScheduleClone
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

CREATE Procedure dbo.TaskScheduleClone
(
		@TaskScheduleId			INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT			= NULL	
	,	@TaskScheduleTypeId		INT								
	,	@TaskEntityId			INT									
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskSchedule'			
)
AS
BEGIN		
	
	SELECT	@ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		,	@TaskScheduleTypeId	= ISNULL(@TaskScheduleTypeId, TaskScheduleTypeId)
		,	@TaskEntityId		= ISNULL(@TaskEntityId, TaskEntityId)			
	FROM	dbo.TaskSchedule
	WHERE	TaskScheduleId		= @TaskScheduleId

	EXEC dbo.TaskScheduleInsert 
			@TaskScheduleId		=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@TaskScheduleTypeId	=	@TaskScheduleTypeId
		,	@TaskEntityId		=	@TaskEntityId
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
