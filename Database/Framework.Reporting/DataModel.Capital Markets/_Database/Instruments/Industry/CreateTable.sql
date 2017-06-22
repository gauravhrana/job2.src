IF OBJECT_ID ('dbo.Industry') IS NOT NULL
	DROP TABLE dbo.Industry
GO

CREATE TABLE dbo.Industry
(
		IndustryId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	Code				VARCHAR(100)		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
