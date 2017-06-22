
ALTER TABLE dbo.FieldConfigurationModeXApplicationRole
	ADD CONSTRAINT FK_FieldConfigurationModeXApplicationRole_FieldConfigurationMode FOREIGN KEY
	(
		FieldConfigurationModeId
	)
	REFERENCES dbo.FieldConfigurationMode
	(
		FieldConfigurationModeId
	)

	
ALTER TABLE dbo.FieldConfigurationModeXApplicationRole
	ADD CONSTRAINT FK_FieldConfigurationModeXApplicationRole_FieldConfigurationModeAccessMode FOREIGN KEY
	(
		FieldConfigurationModeAccessModeId
	)
	REFERENCES dbo.FieldConfigurationModeAccessMode
	(
		FieldConfigurationModeAccessModeId
	)