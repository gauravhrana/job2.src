IF OBJECT_ID ('dbo.PortfolioXCustodianAccount') IS NOT NULL
	DROP TABLE dbo.PortfolioXCustodianAccount
GO

CREATE TABLE dbo.PortfolioXCustodianAccount
(
		PortfolioXCustodianAccountId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	CustodianAccountId			INT		NOT NULL
	,	PortfolioId			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
