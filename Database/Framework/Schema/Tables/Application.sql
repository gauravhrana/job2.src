IF OBJECT_ID ('dbo.Application') IS NOT NULL
	DROP TABLE dbo.Application
GO

CREATE TABLE dbo.Application
(
		ApplicationId			INT				NOT NULL 	
	,	Name					VARCHAR (50)	NOT NULL	
	,	Code					VARCHAR (50)	NOT NULL	
	,	Description				VARCHAR (500)	NOT NULL	
	,	SortOrder				INT				NOT NULL
);

