IF OBJECT_ID ('dbo.Report') IS NOT NULL
	DROP TABLE dbo.Report
GO

CREATE TABLE dbo.Report 
(
		ReportId		INT				NOT NULL
	,	ApplicationId	INT				NOT NULL
    ,	Name			VARCHAR (50)	NOT NULL	
    ,	Description     VARCHAR (500)	NOT NULL	
	,	Title			VARCHAR (50)	NOT NULL
    ,	SortOrder		INT				NOT NULL	
);

