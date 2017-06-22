IF OBJECT_ID ('dbo.SystemEntityCategory') IS NOT NULL
	DROP TABLE dbo.SystemEntityCategory
GO

CREATE TABLE dbo.SystemEntityCategory 
(
		SystemEntityCategoryId		INT				NOT NULL
	,   ApplicationId				INT				NOT NULL 	
	,	Name						VARCHAR (50)	NOT NULL	
	,	Description					VARCHAR (50)	NOT NULL	
	,	SortOrder					INT				NOT NULL
);

