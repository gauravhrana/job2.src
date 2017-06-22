IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleTypeDelete')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleTypeDelete'
	DROP  Procedure TaskScheduleTypeDelete
END
GO

PRINT 'Creating Procedure TaskScheduleTypeDelete'
GO
/******************************************************************************
**		File: 
**		Name: TaskScheduleTypeDelete
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
CREATE Procedure dbo.TaskScheduleTypeDelete
(
		@TaskScheduleTypeId 	INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskScheduleType'
)
AS
BEGIN

	DELETE	 dbo.TaskScheduleType
	WHERE	 TaskScheduleTypeId = @TaskScheduleTypeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleTypeId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
