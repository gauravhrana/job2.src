








ALTER TABLE dbo.SecurityXExternalMarketDataIdentifier
	ADD CONSTRAINT FK_SecurityXExternalMarketDataIdentifier_Security FOREIGN KEY
	(
		SecurityId
	)
	REFERENCES dbo.Security
	(
		SecurityId
	)
GO




