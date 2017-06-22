IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleUpdate')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleUpdate'
	DROP  Procedure  TaskScheduleUpdate
END
GO

PRINT 'Creating Procedure TaskScheduleUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TaskScheduleUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TaskScheduleUpdate
(
		@TaskScheduleId			INT	
	,	@TaskScheduleTypeId		INT					
	,	@TaskEntityId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'TaskSchedule'
)
AS
BEGIN 

	UPDATE	dbo.TaskSchedule 
	SET		TaskScheduleTypeId	=	@TaskScheduleTypeId	
		,	TaskEntityId		=	@TaskEntityId						
	WHERE	TaskScheduleId		=	@TaskScheduleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TaskScheduleId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END		
 GO