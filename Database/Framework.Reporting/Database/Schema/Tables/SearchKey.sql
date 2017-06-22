IF OBJECT_ID ('dbo.SearchKey') IS NOT NULL
	DROP TABLE dbo.SearchKey
GO

CREATE TABLE dbo.SearchKey 
(
		SearchKeyId			INT				IDENTITY(1, 1)	NOT NULL 
	,   ApplicationId		INT								NOT NULL 	
	,	Name				VARCHAR (50)					NOT NULL
	,	[View]				VARCHAR(200)					NOT NULL		
	,	[Description]		VARCHAR (500)					NOT NULL	
	,	SortOrder			INT								NOT NULL		
);

