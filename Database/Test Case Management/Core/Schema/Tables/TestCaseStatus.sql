IF OBJECT_ID ('dbo.TestCaseStatus') IS NOT NULL
	DROP TABLE dbo.TestCaseStatus
GO

CREATE TABLE dbo.TestCaseStatus 
(
		TestCaseStatusId			INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (100)	NOT NULL	
   	,	SortOrder			INT				NOT NULL
);

