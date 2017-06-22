IF OBJECT_ID ('dbo.TestCasePriority') IS NOT NULL
	DROP TABLE dbo.TestCasePriority
GO

CREATE TABLE dbo.TestCasePriority 
(
		TestCasePriorityId			INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (100)	NOT NULL	
   	,	SortOrder			INT				NOT NULL
);

