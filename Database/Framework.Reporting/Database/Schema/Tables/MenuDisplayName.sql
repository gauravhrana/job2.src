IF OBJECT_ID ('dbo.MenuDisplayName') IS NOT NULL
	DROP TABLE dbo.MenuDisplayName
GO

CREATE TABLE dbo.MenuDisplayName 
(
		MenuDisplayNameId	INT					NOT NULL 
	,   ApplicationId		INT					NOT NULL 
	,	LanguageId			INT					NOT NULL
	,	MenuId				INT					NOT NULL	
	,	Value				VARCHAR (50)		NOT NULL	
	,	IsDefault			INT					NOT NULL
);