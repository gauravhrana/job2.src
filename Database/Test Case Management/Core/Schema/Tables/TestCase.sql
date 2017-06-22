IF OBJECT_ID ('dbo.TestCase') IS NOT NULL
	DROP TABLE dbo.TestCase
GO

CREATE TABLE dbo.TestCase 
(
		TestCaseId			INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (100)	NOT NULL	
   	,	SortOrder			INT				NOT NULL
);

