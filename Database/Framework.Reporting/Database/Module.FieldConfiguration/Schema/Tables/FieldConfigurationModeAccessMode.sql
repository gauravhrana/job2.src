IF OBJECT_ID ('dbo.FieldConfigurationModeAccessMode') IS NOT NULL
	DROP TABLE dbo.FieldConfigurationModeAccessMode
GO

CREATE TABLE dbo.FieldConfigurationModeAccessMode
(
		FieldConfigurationModeAccessModeId		INT				NOT NULL
	,   ApplicationId							INT				NOT NULL 	
	,	Name									VARCHAR (50)	NOT NULL	
	,	Description								VARCHAR (500)	NOT NULL	
	,	SortOrder								INT				NOT NULL
);

