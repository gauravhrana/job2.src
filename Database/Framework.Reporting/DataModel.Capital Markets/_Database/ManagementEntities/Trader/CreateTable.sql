IF OBJECT_ID ('dbo.Trader') IS NOT NULL
	DROP TABLE dbo.Trader
GO

CREATE TABLE dbo.Trader
(
		TraderId			INT		NOT NULL
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
