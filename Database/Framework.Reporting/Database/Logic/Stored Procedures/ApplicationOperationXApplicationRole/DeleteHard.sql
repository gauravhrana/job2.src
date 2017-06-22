IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleDeleteHard'
	DROP  Procedure ApplicationOperationXApplicationRoleDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationOperationXApplicationRoleDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ApplicationOperationXApplicationRoleDelete
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
CREATE Procedure dbo.ApplicationOperationXApplicationRoleDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'ApplicationOperationXApplicationRole'	 
)
AS
BEGIN

	IF @KeyType = 'ApplicationOperationXApplicationRoleId'
		BEGIN

			DELETE	 dbo.ApplicationOperationXApplicationRole
			WHERE	 ApplicationOperationXApplicationRoleId = @KeyId

		END
	ELSE IF @KeyType = 'ApplicationOperation'
		BEGIN

			DELETE	 dbo.ApplicationOperationXApplicationRole
			WHERE	 ApplicationOperationId = @KeyId

		END
	ELSE IF @KeyType = 'ApplicationRoleId'
		BEGIN

			DELETE	 dbo.ApplicationOperationXApplicationRole
			WHERE	 ApplicationRoleId = @KeyId

		END
	ELSE IF @KeyType = 'ApplicationId'
		BEGIN

			DELETE	 dbo.ApplicationOperationXApplicationRole
			WHERE	 ApplicationOperationId IN
			(
				SELECT ApplicationOperationId
				FROM  dbo. dbo.ApplicationOperation 
				WHERE ApplicationOperationId = @KeyId	
			)
	END
	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
