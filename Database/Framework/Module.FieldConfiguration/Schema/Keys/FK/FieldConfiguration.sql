ALTER TABLE dbo.FieldConfiguration
	ADD CONSTRAINT FK_FieldConfiguration_FieldConfigurationModeId FOREIGN KEY
	(
		FieldConfigurationModeId
	)
	REFERENCES dbo.FieldConfigurationMode
	(
		FieldConfigurationModeId 
	)
GO