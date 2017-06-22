IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleDeleteHard'
	DROP  Procedure TaskScheduleDeleteHard
END
GO

PRINT 'Creating Procedure TaskScheduleDeleteHard'
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
CREATE Procedure dbo.TaskScheduleDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskSchedule'
)
AS
BEGIN

	IF @KeyType = 'TaskScheduleId'
		BEGIN

			EXEC	dbo.TaskRunDeleteHard 
					@KeyId		=	@KeyId, 
					@KeyType	=	'TaskScheduleId',
					@AuditId	=	@AuditId

			DELETE	 dbo.TaskSchedule
			WHERE	 TaskScheduleId = @KeyId

		END
	ELSE IF @KeyType = 'TaskScheduleTypeId'
		BEGIN

			EXEC	dbo.TaskRunDeleteHard 
					@KeyId		=	@KeyId, 
					@KeyType	=	'TaskScheduleTypeId',
					@AuditId	=	@AuditId

			DELETE	 dbo.TaskSchedule
			WHERE	 TaskScheduleTypeId = @KeyId

		END
	ELSE IF @KeyType = 'TaskEntityId'
		BEGIN

			EXEC	dbo.TaskRunDeleteHard 
					@KeyId		=	@KeyId, 
					@KeyType	=	'TaskEntityId',
					@AuditId	=	@AuditId

			DELETE	 dbo.TaskSchedule
			WHERE	 TaskEntityId = @KeyId

		END
	ELSE IF @KeyType = 'TaskEntityTypeId'
		BEGIN

			EXEC	dbo.TaskRunDeleteHard 
					@KeyId		=	@KeyId, 
					@KeyType	=	'TaskEntityTypeId',
					@AuditId	=	@AuditId

			DELETE	 dbo.TaskSchedule
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
