
ALTER TABLE dbo.CustodianAccount
	ADD CONSTRAINT FK_CustodianAccount_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







