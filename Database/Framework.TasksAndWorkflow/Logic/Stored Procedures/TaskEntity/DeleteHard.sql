IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TaskEntityDeleteHard'
	DROP  Procedure TaskEntityDeleteHard
END
GO

PRINT 'Creating Procedure TaskEntityDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: TaskEntityDelete
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
CREATE Procedure dbo.TaskEntityDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'TaskEntity'
)
AS
BEGIN

	IF @KeyType = 'TaskEntityId'
		BEGIN

			EXEC	dbo.TaskRunDeleteHard 
					@KeyId		=	@KeyId, 
					@KeyType	=	'TaskEntityId',
					@AuditId	=	@AuditId

			EXEC	dbo.TaskScheduleDeleteHard 
					@KeyId		=	@KeyId, 
					@KeyType	=	'TaskEntityId',
					@AuditId	=	@AuditId

			DELETE	 dbo.TaskEntity
			WHERE	 TaskEntityId = @KeyId

		END
	ELSE IF @KeyType = 'TaskEntityTypeId'
		BEGIN

			EXEC	dbo.TaskRunDeleteHard 
					@KeyId		=	@KeyId, 
					@KeyType	=	'TaskEntityTypeId',
					@AuditId	=	@AuditId

			EXEC	dbo.TaskScheduleDeleteHard 
					@KeyId		=	@KeyId, 
					@KeyType	=	'TaskEntityTypeId',
					@AuditId	=	@AuditId

			DELETE	 dbo.TaskEntity
			WHERE	 TaskEntityTypeId = @KeyId


	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
