IF OBJECT_ID ('dbo.Module') IS NOT NULL
	DROP TABLE dbo.Module
GO

CREATE TABLE dbo.Module
(
		ModuleId			INT				NOT NULL
	,   ApplicationId		INT				NOT NULL 	
	,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (50)	NOT NULL	
	,	SortOrder			INT				NOT NULL
);

