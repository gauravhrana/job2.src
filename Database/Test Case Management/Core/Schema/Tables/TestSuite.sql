IF OBJECT_ID ('dbo.TestSuite') IS NOT NULL
	DROP TABLE dbo.TestSuite
GO

CREATE TABLE dbo.TestSuite 
(
		TestSuiteId			INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (100)	NOT NULL	
   	,	SortOrder			INT				NOT NULL
);

