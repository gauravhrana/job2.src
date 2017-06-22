IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleChildrenGet')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleChildrenGet'
	DROP  Procedure TaskScheduleChildrenGet
END
GO

PRINT 'Creating Procedure TaskScheduleChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: TaskScheduleChildrenGet
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

CREATE Procedure dbo.TaskScheduleChildrenGet
(
		@TaskScheduleId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'TaskSchedule'
)
AS
BEGIN

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
	WHERE	a.TaskScheduleId = @TaskScheduleId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   