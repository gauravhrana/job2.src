IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityChildrenGet')
BEGIN
	PRINT 'Dropping Procedure TaskEntityChildrenGet'
	DROP  Procedure TaskEntityChildrenGet
END
GO

PRINT 'Creating Procedure TaskEntityChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: TaskEntityChildrenGet
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TaskEntityChildrenGet
(
		@TaskEntityId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'TaskEntity'
)
AS
BEGIN

	-- GET TaskSchedule Records
	SELECT	a.TaskScheduleId	
		,	a.ApplicationId	
		,	a.TaskScheduleTypeId		
		,	a.TaskEntityId		
		,	b.Name		AS	'TaskScheduleType'				
		,	c.Name		AS	'TaskEntity'
	FROM		dbo.TaskSchedule a
	INNER JOIN	dbo.TaskScheduleType b ON a.TaskScheduleTypeId	= b.TaskScheduleTypeId
	INNER JOIN	dbo.TaskEntity		 c ON a.TaskEntityId		= c.TaskEntityId
	WHERE	a.TaskEntityId = @TaskEntityId

	-- GET TaskRun Records
	SELECT	a.TaskRunId	
		,	a.ApplicationId	
		,	a.TaskScheduleId	
		,	a.TaskEntityId	
		,	a.BusinessDate	
		,	a.StartTime		
		,	a.EndTime		
		,	a.RunBy				
		,	b.Name		AS	'TaskEntity'
	FROM	TaskRun a
	INNER JOIN	dbo.TaskEntity b ON a.TaskEntityId		= b.TaskEntityId
	WHERE	a.TaskEntityId = @TaskEntityId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskEntityId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   