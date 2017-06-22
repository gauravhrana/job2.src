IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleDeleteHard'
	DROP  Procedure ApplicationUserXApplicationRoleDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationUserXApplicationRoleDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ApplicationUserXApplicationRoleDelete
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
CREATE Procedure dbo.ApplicationUserXApplicationRoleDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT		 = NULL				
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'ApplicationUserXApplicationRole'	 
)
AS
BEGIN

	IF @KeyType = 'ApplicationUserXApplicationRoleId'
		BEGIN

			DELETE	 dbo.ApplicationUserXApplicationRole
			WHERE	 ApplicationUserXApplicationRoleId = @KeyId

		END
	ELSE IF @KeyType = 'ApplicationUser'
		BEGIN

			DELETE	 dbo.ApplicationUserXApplicationRole
			WHERE	 ApplicationUserId = @KeyId

		END
	ELSE IF @KeyType = 'ApplicationRoleId'
		BEGIN

			DELETE	 dbo.ApplicationUserXApplicationRole
			WHERE	 ApplicationRoleId = @KeyId

		END
	ELSE IF @KeyType = 'ApplicationId'
		BEGIN

			DELETE	 dbo.ApplicationUserXApplicationRole
			WHERE	 ApplicationUserId IN
			(
				SELECT ApplicationUserId
				FROM  dbo. dbo.ApplicationUser 
				WHERE ApplicationUserId = @KeyId	
			)

	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
