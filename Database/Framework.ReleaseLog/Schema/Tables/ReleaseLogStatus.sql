IF OBJECT_ID ('dbo.ReleaseLogStatus') IS NOT NULL
	DROP TABLE dbo.ReleaseLogStatus 
GO


CREATE TABLE dbo.ReleaseLogStatus 
(
		ReleaseLogStatusId		  INT				NOT NULL
	,	ApplicationId			  INT				NOT NULL
	,	Name					  VARCHAR (50)		NOT NULL
	,	Description				  VARCHAR (500)	NOT NULL
	,	SortOrder				  INT				NOT NULL
);

