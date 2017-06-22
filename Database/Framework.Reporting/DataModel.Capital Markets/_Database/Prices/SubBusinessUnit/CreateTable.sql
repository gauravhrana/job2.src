IF OBJECT_ID ('dbo.SubBusinessUnit') IS NOT NULL
	DROP TABLE dbo.SubBusinessUnit
GO

CREATE TABLE dbo.SubBusinessUnit
(
		SubBusinessUnitId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	FundId			INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
