IF OBJECT_ID ('dbo.SearchKeyDetailItem') IS NOT NULL
	DROP TABLE dbo.SearchKeyDetailItem
GO

CREATE TABLE dbo.SearchKeyDetailItem 
(
		SearchKeyDetailItemId INT IDENTITY(1, 1)	NOT NULL
	,   ApplicationId		  INT					NOT NULL 
	,	SearchKeyDetailId	  INT					NOT NULL
	,	[Value]				  VARCHAR(200)			NOT NULL
	,	SortOrder			  INT
);

