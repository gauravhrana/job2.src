IF OBJECT_ID ('dbo.PortfolioGroupRules') IS NOT NULL
	DROP TABLE dbo.PortfolioGroupRules
GO

CREATE TABLE dbo.PortfolioGroupRules
(
		PortfolioGroupRulesId			INT		NOT NULL
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
