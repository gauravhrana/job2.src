IF OBJECT_ID ('dbo.FieldConfigurationModeXApplicationRole') IS NOT NULL
	DROP TABLE dbo.FieldConfigurationModeXApplicationRole
GO


CREATE TABLE dbo.FieldConfigurationModeXApplicationRole
(
	FieldConfigurationModeXApplicationRoleId	INT				NOT NULL,
	ApplicationId								INT				NOT NULL,
	FieldConfigurationModeId					INT				NOT NULL,
	ApplicationRoleId							INT				NOT NULL,
	FieldConfigurationModeAccessModeId			INT				NOT NULL
)
GO