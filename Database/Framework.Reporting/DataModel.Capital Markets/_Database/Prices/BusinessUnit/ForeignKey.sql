
ALTER TABLE dbo.BusinessUnit
	ADD CONSTRAINT FK_BusinessUnit_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







