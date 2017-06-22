IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TaskScheduleDoesExist')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleDoesExist'
	DROP  Procedure  TaskScheduleDoesExist
END
GO

PRINT 'Creating Procedure TaskScheduleDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TaskScheduleDoesExist
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

Create procedure dbo.TaskScheduleDoesExist
(
		@TaskScheduleTypeId		INT							
	,	@TaskEntityId			INT	
	,	@TaskScheduleId			INT							
	,	@AuditId				INT							
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskSchedule'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.TaskSchedule a
	WHERE		a.TaskScheduleTypeId	=	@TaskScheduleTypeId
	AND			a.TaskEntityId			=	@TaskEntityId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

