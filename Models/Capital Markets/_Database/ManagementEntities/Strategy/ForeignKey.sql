
ALTER TABLE dbo.Strategy
	ADD CONSTRAINT FK_Strategy_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







