IF OBJECT_ID ('dbo.FieldConfigurationCategory') IS NOT NULL
	DROP TABLE dbo.FieldConfigurationCategory
GO

CREATE TABLE dbo.FieldConfigurationCategory
(
		FieldConfigurationCategoryId		INT				NOT NULL
	,   ApplicationId						INT				NOT NULL 	
	,	Name								VARCHAR (100)	NOT NULL	
	,	Description							VARCHAR (500)	NOT NULL	
	,	SortOrder							INT				NOT NULL
);

