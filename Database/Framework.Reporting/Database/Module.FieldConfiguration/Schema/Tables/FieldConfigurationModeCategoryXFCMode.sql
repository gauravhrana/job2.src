IF OBJECT_ID ('dbo.FieldConfigurationModeCategoryXFCMode') IS NOT NULL
	DROP TABLE dbo.FieldConfigurationModeCategoryXFCMode
GO


CREATE TABLE dbo.FieldConfigurationModeCategoryXFCMode
	(
	FieldConfigurationModeCategoryXFCModeId				INT				NOT NULL,
	ApplicationId						INT				NOT NULL,
	FieldConfigurationModeId			INT				NOT NULL,
	FieldConfigurationModeCategoryId	INT				NOT NULL
	)
GO