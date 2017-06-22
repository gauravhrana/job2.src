ALTER TABLE dbo.FieldConfigurationDisplayName
	ADD CONSTRAINT FK_FieldConfigurationDisplayName_FieldConfiguration FOREIGN KEY
	(
		FieldConfigurationId
	)
	REFERENCES dbo.FieldConfiguration
	(
		FieldConfigurationId 
	)
GO

ALTER TABLE dbo.FieldConfigurationDisplayName
	ADD CONSTRAINT FK_FieldConfigurationDisplayName_Language FOREIGN KEY
	(
		LanguageId
	)
	REFERENCES dbo.Language
	(
		LanguageId 
	)
GO