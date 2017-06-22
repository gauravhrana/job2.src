IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleDelete')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleDelete'
	DROP  Procedure TaskScheduleDelete
END
GO

PRINT 'Creating Procedure TaskScheduleDelete'
GO
/******************************************************************************
**		File: 
**		Name: TaskScheduleDelete
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
CREATE Procedure dbo.TaskScheduleDelete
(
		@TaskScheduleId 		INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskSchedule'
)
AS
BEGIN

	DELETE	 dbo.TaskSchedule
	WHERE	 TaskScheduleId = @TaskScheduleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
