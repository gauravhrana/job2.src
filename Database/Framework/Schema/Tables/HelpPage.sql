IF OBJECT_ID ('dbo.HelpPage') IS NOT NULL
	DROP TABLE dbo.HelpPage
GO

CREATE TABLE dbo.HelpPage 
(
		HelpPageId			INT				IDENTITY(1, 1)	NOT NULL 
	,   ApplicationId		INT								NOT NULL 
	,	SystemEntityTypeId	INT								NOT NULL
	,	HelpPageContextId	INT								NOT NULL	
	,	Name				VARCHAR (50)					NOT NULL	
	,	Content				VARCHAR (MAX)					NOT NULL	
	,	SortOrder			INT								NOT NULL
);