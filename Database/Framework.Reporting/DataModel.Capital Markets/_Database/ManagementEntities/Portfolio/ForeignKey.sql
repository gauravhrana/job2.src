
ALTER TABLE dbo.Portfolio
	ADD CONSTRAINT FK_Portfolio_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







