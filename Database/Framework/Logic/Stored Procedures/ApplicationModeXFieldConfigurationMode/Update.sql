CREATE PROCEDURE dbo.ApplicationModeXFieldConfigurationModeUpdate
(
		@ApplicationModeXFieldConfigurationModeId		INT		
	,	@ApplicationId									INT
	,	@ApplicationModeId								INT					
	,	@FieldConfigurationModeId						INT					
	,	@AuditId										INT					
	,	@AuditDate										DATETIME	= NULL	
	,	@SystemEntityType								VARCHAR(50)	= 'ApplicationModeXFieldConfigurationMode'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationModeXFieldConfigurationMode 
	SET		ApplicationModeId										=	@ApplicationModeId		
		,	FieldConfigurationModeId						=	@FieldConfigurationModeId							
	WHERE	ApplicationModeXFieldConfigurationModeId		=	@ApplicationModeXFieldConfigurationModeId
	AND		ApplicationId											=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationModeXFieldConfigurationModeId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

