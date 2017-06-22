IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ActivityTypeDeleteHard'
	DROP  Procedure ActivityTypeDeleteHard
END
GO

PRINT 'Creating Procedure ActivityTypeDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: ActivityTypeDelete
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

CREATE Procedure dbo.ActivityTypeDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'ActivityType'
)
AS
BEGIN

	IF @KeyType = 'ActivityTypeId'
		BEGIN

			EXEC	dbo.ActivityDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'ActivityTypeId'
				,	@AuditId 	=	@AuditId

			EXEC	dbo.ActivitySubTypeDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'ActivityTypeId'
				,	@AuditId 	=	@AuditId

			DELETE	 dbo.ActivityType
			WHERE	 ActivityTypeId = @KeyId	

		END	


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
