IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleTypeDeleteHard'
	DROP  Procedure TaskScheduleTypeDeleteHard
END
GO

PRINT 'Creating Procedure TaskScheduleTypeDeleteHard'
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
CREATE Procedure dbo.TaskScheduleTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskScheduleType'
)
AS
BEGIN

	IF @KeyType = 'TaskScheduleTypeId'
	BEGIN

		EXEC	dbo.TaskScheduleDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'TaskScheduleTypeId',
				@AuditId	=	@AuditId

		DELETE	 dbo.TaskScheduleType
		WHERE	 TaskScheduleTypeId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
