
ALTER TABLE dbo.SubClass
	ADD CONSTRAINT FK_SubClass_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







