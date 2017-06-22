IF OBJECT_ID ('dbo.Sector') IS NOT NULL
	DROP TABLE dbo.Sector
GO

CREATE TABLE dbo.Sector
(
		SectorId			INT		NOT NULL
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
