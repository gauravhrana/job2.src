IF OBJECT_ID ('dbo.ReleaseIssueType') IS NOT NULL
	DROP TABLE dbo.ReleaseIssueType 
GO


CREATE TABLE dbo.ReleaseIssueType 
(
		ReleaseIssueTypeId		  INT				NOT NULL
	,	ApplicationId			  INT				NOT NULL
	,	Name					  VARCHAR (50)		NOT NULL
	,	Description				  VARCHAR (500)	NOT NULL
	,	SortOrder				  INT				NOT NULL
);

