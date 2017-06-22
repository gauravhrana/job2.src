IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskRunList')
BEGIN
	PRINT 'Dropping Procedure TaskRunList'
	DROP  Procedure  dbo.TaskRunList
END
GO

PRINT 'Creating Procedure TaskRunList'
GO

/******************************************************************************
**		File: 
**		Name: TaskRunList
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

CREATE Procedure dbo.TaskRunList
(
		@AuditId				INT	
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskRun'
)
AS
BEGIN

	SELECT	a.TaskRunId		
		,	a.ApplicationId
		,	a.TaskScheduleId	
		,	a.TaskEntityId	
		,	a.BusinessDate	
		,	a.StartTime		
		,	a.EndTime		
		,	a.RunBy				
		,	b.Name		AS	'TaskEntity'
	FROM	dbo.TaskRun a
	INNER JOIN	dbo.TaskEntity b ON a.TaskEntityId		= b.TaskEntityId
	WHERE	a.ApplicationId = @ApplicationId
	ORDER BY    a.TaskRunId				ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO