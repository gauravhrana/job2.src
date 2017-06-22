IF OBJECT_ID ('dbo.FundXPortfolio') IS NOT NULL
	DROP TABLE dbo.FundXPortfolio
GO

CREATE TABLE dbo.FundXPortfolio
(
		FundXPortfolioId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	FundId			INT		NOT NULL
	,	PortfolioId			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
