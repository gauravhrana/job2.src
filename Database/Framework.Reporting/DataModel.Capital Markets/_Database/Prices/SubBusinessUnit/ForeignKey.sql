
ALTER TABLE dbo.SubBusinessUnit
	ADD CONSTRAINT FK_SubBusinessUnit_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







