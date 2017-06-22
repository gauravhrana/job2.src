IF OBJECT_ID ('dbo.SearchKeyDetail') IS NOT NULL
	DROP TABLE dbo.SearchKeyDetail
GO

CREATE TABLE dbo.SearchKeyDetail 
(
		SearchKeyDetailId	INT		IDENTITY(1, 1)	NOT NULL
	,   ApplicationId		INT						NOT NULL 
	,	SearchKeyId			INT						NOT NULL 
	,	SearchParameter		VARCHAR(200)			NOT NULL	
	,	SortOrder			INT
);

