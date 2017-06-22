IF OBJECT_ID ('dbo.Language') IS NOT NULL
	DROP TABLE dbo.Language
GO

CREATE TABLE dbo.Language
(
		LanguageId			INT				NOT NULL
	,   ApplicationId		INT				NOT NULL 	
	,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (50)	NOT NULL	
	,	SortOrder			INT				NOT NULL
);

