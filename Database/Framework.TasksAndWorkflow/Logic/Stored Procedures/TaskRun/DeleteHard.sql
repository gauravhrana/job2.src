IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskRunDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TaskRunDeleteHard'
	DROP  Procedure TaskRunDeleteHard
END
GO

PRINT 'Creating Procedure TaskRunDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: TaskRunDelete
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
CREATE Procedure dbo.TaskRunDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskRun'
)
AS
BEGIN

	IF @KeyType = 'TaskRunId'
		BEGIN

			DELETE	 dbo.TaskRun
			WHERE	 TaskRunId = @KeyId

		END
	ELSE IF @KeyType = 'TaskScheduleId'
		BEGIN

			DELETE	 dbo.TaskRun
			WHERE	 TaskScheduleId = @KeyId

		END
	ELSE IF @KeyType = 'TaskEntityId'
		BEGIN

			DELETE	 dbo.TaskRun
			WHERE	 TaskEntityId = @KeyId

		END
	ELSE IF @KeyType = 'TaskScheduleTypeId'
		BEGIN

			DELETE	 dbo.TaskRun
			WHERE	 TaskScheduleId IN
			(
				SELECT	TaskScheduleId	
				FROM	dbo.TaskSchedule
				WHERE	TaskScheduleTypeId = @KeyId
			)

		END
	ELSE IF @KeyType = 'TaskEntityTypeId'
		BEGIN

			DELETE	 dbo.TaskRun
			WHERE	 TaskEntityId IN
			(
				SELECT	TaskEntityId	
				FROM	dbo.TaskEntity
				WHERE	TaskEntityTypeId = @KeyId
			)

	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
