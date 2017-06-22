ALTER TABLE dbo.FieldConfigurationModeCategoryXFCMode
	ADD CONSTRAINT FK_FieldConfigurationModeCategoryXFCMode_FieldConfigurationModeCategory FOREIGN KEY
	(
		FieldConfigurationModeCategoryId
	)
	REFERENCES dbo.FieldConfigurationModeCategory
	(
		FieldConfigurationModeCategoryId
	)
GO


ALTER TABLE dbo.FieldConfigurationModeCategoryXFCMode
	ADD CONSTRAINT FK_FieldConfigurationModeCategoryXFCMode_FieldConfigurationMode FOREIGN KEY
	(
		FieldConfigurationModeId
	)
	REFERENCES dbo.FieldConfigurationMode
	(
		FieldConfigurationModeId
	)
GO