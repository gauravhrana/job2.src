IF OBJECT_ID ('dbo.TestRun') IS NOT NULL
	DROP TABLE dbo.TestRun
GO

CREATE TABLE dbo.TestRun 
(
		TestRunId			INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (100)	NOT NULL	
   	,	SortOrder			INT				NOT NULL
);

