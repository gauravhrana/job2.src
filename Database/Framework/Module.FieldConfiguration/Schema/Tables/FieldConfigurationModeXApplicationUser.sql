IF OBJECT_ID ('dbo.FieldConfigurationModeXApplicationUser') IS NOT NULL
	DROP TABLE dbo.FieldConfigurationModeXApplicationUser
GO


CREATE TABLE dbo.FieldConfigurationModeXApplicationUser
(
	FieldConfigurationModeXApplicationUserId	INT				NOT NULL,
	ApplicationId								INT				NOT NULL,
	FieldConfigurationModeId					INT				NOT NULL,
	ApplicationUserId							INT				NOT NULL,
	FieldConfigurationModeAccessModeId			INT				NOT NULL
)
GO