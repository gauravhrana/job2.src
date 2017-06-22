
ALTER TABLE dbo.InvestingFeeder
	ADD CONSTRAINT FK_InvestingFeeder_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







