IF OBJECT_ID ('dbo.FieldConfigurationDisplayName') IS NOT NULL
	DROP TABLE dbo.FieldConfigurationDisplayName
GO

CREATE TABLE dbo.FieldConfigurationDisplayName 
(
		FieldConfigurationDisplayNameId		INT				IDENTITY(1, 1)	NOT NULL 
	,   ApplicationId						INT								NOT NULL 
	,	LanguageId							INT								NOT NULL
	,	FieldConfigurationId				INT								NOT NULL	
	,	Value								VARCHAR (200)					NOT NULL	
	,	IsDefault							INT								NOT NULL
);