/******************************************************************************
**		Task: 
**		Name: ApplicationModeXFieldConfigurationModeDelete
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
CREATE PROCEDURE dbo.ApplicationModeXFieldConfigurationModeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'ApplicationModeXFieldConfigurationMode'	 
)
AS
BEGIN

	IF @KeyType = 'ApplicationModeXFieldConfigurationModeId'
		BEGIN

			DELETE	 dbo.ApplicationModeXFieldConfigurationMode
			WHERE	 ApplicationModeXFieldConfigurationModeId = @KeyId

		END
	ELSE IF @KeyType = 'ApplicationMode'
		BEGIN

			DELETE	 dbo.ApplicationModeXFieldConfigurationMode
			WHERE	 ApplicationModeId = @KeyId

		END
	ELSE IF @KeyType = 'FieldConfigurationModeId'
		BEGIN

			DELETE	 dbo.ApplicationModeXFieldConfigurationMode
			WHERE	 FieldConfigurationModeId = @KeyId

		END
	ELSE IF @KeyType = 'ApplicationId'
		BEGIN

			DELETE	 dbo.ApplicationModeXFieldConfigurationMode
			WHERE	 ApplicationModeId IN
			(
				SELECT ApplicationModeId
				FROM  dbo. dbo.ApplicationMode 
				WHERE ApplicationModeId = @KeyId	
			)
	END
	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END

GO

