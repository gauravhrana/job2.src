IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivitySubTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ActivitySubTypeDeleteHard'
	DROP  Procedure ActivitySubTypeDeleteHard
END
GO

PRINT 'Creating Procedure ActivitySubTypeDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: ActivitySubTypeDelete
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

CREATE Procedure dbo.ActivitySubTypeDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'ActivitySubType'
)
AS
BEGIN

	IF @KeyType = 'ActivitySubTypeId'
		BEGIN

			EXEC	dbo.ActivityDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'ActivitySubTypeId'
				,	@AuditId 	=	@AuditId

			DELETE	 dbo.ActivitySubType
			WHERE	 ActivitySubTypeId = @KeyId	

		END
	ELSE IF @KeyType = 'ActivityTypeId'
		BEGIN

			EXEC	dbo.ActivityDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'ActivityTypeId'
				,	@AuditId 	=	@AuditId

			DELETE	 dbo.ActivitySubType
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
