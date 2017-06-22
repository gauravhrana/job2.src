IF OBJECT_ID ('dbo.ReportCategory') IS NOT NULL
	DROP TABLE dbo.ReportCategory
GO

CREATE TABLE dbo.ReportCategory 
(
		ReportCategoryId	INT				NOT NULL
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
    ,	Description			VARCHAR (500)	NOT NULL
    ,	SortOrder			INT				NOT NULL	
);

