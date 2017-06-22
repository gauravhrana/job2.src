
ALTER TABLE dbo.FieldConfigurationModeXApplicationUser
	ADD CONSTRAINT FK_FieldConfigurationModeXApplicationUser_FieldConfigurationMode FOREIGN KEY
	(
		FieldConfigurationModeId
	)
	REFERENCES dbo.FieldConfigurationMode
	(
		FieldConfigurationModeId
	)

	
ALTER TABLE dbo.FieldConfigurationModeXApplicationUser
	ADD CONSTRAINT FK_FieldConfigurationModeXApplicationUser_FieldConfigurationModeAccessMode FOREIGN KEY
	(
		FieldConfigurationModeAccessModeId
	)
	REFERENCES dbo.FieldConfigurationModeAccessMode
	(
		FieldConfigurationModeAccessModeId
	)