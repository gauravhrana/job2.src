
ALTER TABLE dbo.PortfolioGroupRules
	ADD CONSTRAINT FK_PortfolioGroupRules_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







