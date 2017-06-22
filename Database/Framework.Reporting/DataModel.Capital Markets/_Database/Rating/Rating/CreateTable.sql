IF OBJECT_ID ('dbo.Rating') IS NOT NULL
	DROP TABLE dbo.Rating
GO

CREATE TABLE dbo.Rating
(
		RatingId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	Date				DATETIME		NOT NULL
	,	Analyst				VARCHAR(100)		NOT NULL
	,	Notes				VARCHAR(100)		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
