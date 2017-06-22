
ALTER TABLE dbo.PortfolioXCustodianAccount
	ADD CONSTRAINT FK_PortfolioXCustodianAccount_CustodianAccount FOREIGN KEY
	(
		CustodianAccountId
	)
	REFERENCES dbo.CustodianAccount
	(
		CustodianAccountId
	)
GO

ALTER TABLE dbo.PortfolioXCustodianAccount
	ADD CONSTRAINT FK_PortfolioXCustodianAccount_Portfolio FOREIGN KEY
	(
		PortfolioId
	)
	REFERENCES dbo.Portfolio
	(
		PortfolioId
	)
GO





