ALTER TABLE dbo.FCModeCategoryXFCMode
	ADD CONSTRAINT FK_FCModeCategoryXFCMode_FieldConfigurationModeCategory FOREIGN KEY
	(
		FieldConfigurationModeCategoryId
	)
	REFERENCES dbo.FieldConfigurationModeCategory
	(
		FieldConfigurationModeCategoryId
	)
GO


ALTER TABLE dbo.FCModeCategoryXFCMode
	ADD CONSTRAINT FK_FCModeCategoryXFCMode_FieldConfigurationMode FOREIGN KEY
	(
		FieldConfigurationModeId
	)
	REFERENCES dbo.FieldConfigurationMode
	(
		FieldConfigurationModeId
	)
GO