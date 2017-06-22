IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleTypeChildrenGet')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleTypeChildrenGet'
	DROP  Procedure TaskScheduleTypeChildrenGet
END
GO

PRINT 'Creating Procedure TaskScheduleTypeChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: TaskScheduleTypeChildrenGet
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

CREATE Procedure dbo.TaskScheduleTypeChildrenGet
(
		@TaskScheduleTypeId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'TaskScheduleType'
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
	WHERE	a.TaskScheduleTypeId = @TaskScheduleTypeId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleTypeId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   