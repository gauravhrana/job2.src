










ALTER TABLE dbo.SecurityXInvestmentIdentifier
	ADD CONSTRAINT FK_SecurityXInvestmentIdentifier_Security FOREIGN KEY
	(
		SecurityId
	)
	REFERENCES dbo.Security
	(
		SecurityId
	)
GO




