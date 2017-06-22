IF OBJECT_ID ('dbo.InvestmentPrices') IS NOT NULL
	DROP TABLE dbo.InvestmentPrices
GO

CREATE TABLE dbo.InvestmentPrices
(
		InvestmentPricesId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
