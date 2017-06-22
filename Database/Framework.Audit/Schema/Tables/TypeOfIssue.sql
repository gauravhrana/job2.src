--IF OBJECT_ID ('dbo.TypeOfIssue') IS NOT NULL
--BEGIN
--	DROP TABLE dbo.TypeOfIssue
--END
--GO

CREATE TABLE dbo.TypeOfIssue 
(
		TypeOfIssueId		INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL	
	,	Name				VARCHAR (50)	NOT NULL
	,	Category			VARCHAR(100)	NOT NULL	
	,	Description			VARCHAR (50)	NOT NULL	
	,	SortOrder			INT				NOT NULL
);
