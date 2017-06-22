IF OBJECT_ID ('dbo.FieldConfigurationMode') IS NOT NULL
	DROP TABLE dbo.FieldConfigurationMode
GO

CREATE TABLE dbo.FieldConfigurationMode
(
		FieldConfigurationModeId			INT				NOT NULL
	,   ApplicationId						INT				NOT NULL 	
	,	Name								VARCHAR (50)	NOT NULL	
	,	Description							VARCHAR (500)	NOT NULL	
	,	SortOrder							INT				NOT NULL
);

