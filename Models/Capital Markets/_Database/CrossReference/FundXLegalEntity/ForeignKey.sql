
ALTER TABLE dbo.FundXLegalEntity
	ADD CONSTRAINT FK_FundXLegalEntity_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO

ALTER TABLE dbo.FundXLegalEntity
	ADD CONSTRAINT FK_FundXLegalEntity_LegalEntity FOREIGN KEY
	(
		LegalEntityId
	)
	REFERENCES dbo.LegalEntity
	(
		LegalEntityId
	)
GO





