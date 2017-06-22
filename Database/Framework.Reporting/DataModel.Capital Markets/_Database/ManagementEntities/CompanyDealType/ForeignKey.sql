
ALTER TABLE dbo.CompanyDealType
	ADD CONSTRAINT FK_CompanyDealType_Fund FOREIGN KEY
	(
		FundId
	)
	REFERENCES dbo.Fund
	(
		FundId
	)
GO







