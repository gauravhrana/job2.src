
ALTER TABLE dbo.FundXPortfolio
	ADD CONSTRAINT FK_FundXPortfolio_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO

ALTER TABLE dbo.FundXPortfolio
	ADD CONSTRAINT FK_FundXPortfolio_Portfolio FOREIGN KEY
	(
		PortfolioId
	)
	REFERENCES dbo.Portfolio
	(
		PortfolioId
	)
GO





