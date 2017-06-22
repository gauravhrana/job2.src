IF OBJECT_ID ('dbo.ApplicationMode') IS NOT NULL
	DROP TABLE dbo.ApplicationMode
GO

CREATE TABLE dbo.ApplicationMode
(
		ApplicationModeId		INT				NOT NULL
	,   ApplicationId			INT				NOT NULL 	
	,	Name					VARCHAR (50)	NOT NULL	
	,	Description				VARCHAR (500)	NOT NULL	
	,	SortOrder				INT				NOT NULL
);

