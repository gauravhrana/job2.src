IF OBJECT_ID ('dbo.TestCaseOwner') IS NOT NULL
	DROP TABLE dbo.TestCaseOwner
GO

CREATE TABLE dbo.TestCaseOwner 
(
		TestCaseOwnerId			INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (100)	NOT NULL	
   	,	SortOrder			INT				NOT NULL
);

