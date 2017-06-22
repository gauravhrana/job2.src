
ALTER TABLE dbo.PortfolioType
	ADD CONSTRAINT FK_PortfolioType_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







