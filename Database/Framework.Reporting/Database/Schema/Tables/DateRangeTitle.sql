IF OBJECT_ID ('dbo.DateRangeTitle') IS NOT NULL
	DROP TABLE dbo.DateRangeTitle
GO

CREATE TABLE dbo.DateRangeTitle
(
		DateRangeTitleId		INT				NOT NULL
	,   ApplicationId			INT				NOT NULL 	
	,	Name					VARCHAR (50)	NOT NULL	
	,	[Description]			VARCHAR (500)	NOT NULL	
	,	SortOrder				INT				NOT NULL
);

