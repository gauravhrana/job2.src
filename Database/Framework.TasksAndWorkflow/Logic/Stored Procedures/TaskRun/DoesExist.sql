IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TaskRunDoesExist')
BEGIN
	PRINT 'Dropping Procedure TaskRunDoesExist'
	DROP  Procedure  TaskRunDoesExist
END
GO

PRINT 'Creating Procedure TaskRunDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TaskRunDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.TaskRunDoesExist
(
		@TaskScheduleId			INT	
	,	@TaskRunId				INT						
	,	@TaskEntityId			INT							
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskRun'	
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.TaskRun a
	WHERE		a.TaskScheduleId	=	@TaskScheduleId
	AND			a.TaskEntityId		=	@TaskEntityId

	--Create Audit Record@task
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

