
ALTER TABLE dbo.Trader
	ADD CONSTRAINT FK_Trader_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







