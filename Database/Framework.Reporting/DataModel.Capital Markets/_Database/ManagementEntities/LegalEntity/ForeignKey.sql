
ALTER TABLE dbo.LegalEntity
	ADD CONSTRAINT FK_LegalEntity_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







