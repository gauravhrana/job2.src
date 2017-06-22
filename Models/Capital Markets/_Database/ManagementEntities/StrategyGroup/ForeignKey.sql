
ALTER TABLE dbo.StrategyGroup
	ADD CONSTRAINT FK_StrategyGroup_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO















