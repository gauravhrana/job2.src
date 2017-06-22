
ALTER TABLE dbo.SecurityXParty
	ADD CONSTRAINT FK_SecurityXParty_Exchange FOREIGN KEY
	(
		ExchangeId
	)
	REFERENCES dbo.Exchange
	(
		ExchangeId
	)
GO


ALTER TABLE dbo.SecurityXParty
	ADD CONSTRAINT FK_SecurityXParty_Issuer FOREIGN KEY
	(
		IssuerId
	)
	REFERENCES dbo.Issuer
	(
		IssuerId
	)
GO


ALTER TABLE dbo.SecurityXParty
	ADD CONSTRAINT FK_SecurityXParty_DeliveryAgent FOREIGN KEY
	(
		DeliveryAgentId
	)
	REFERENCES dbo.DeliveryAgent
	(
		DeliveryAgentId
	)
GO


ALTER TABLE dbo.SecurityXParty
	ADD CONSTRAINT FK_SecurityXParty_Security FOREIGN KEY
	(
		SecurityId
	)
	REFERENCES dbo.Security
	(
		SecurityId
	)
GO




