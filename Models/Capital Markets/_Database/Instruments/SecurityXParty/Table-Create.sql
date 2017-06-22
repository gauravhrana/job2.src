IF OBJECT_ID ('dbo.SecurityXParty') IS NOT NULL
	DROP TABLE dbo.SecurityXParty
GO

CREATE TABLE dbo.SecurityXParty
(
		SecurityXPartyId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	ExchangeId				INT		NOT NULL
	,	IssuerId				INT		NOT NULL
	,	DeliveryAgentId				INT		NOT NULL
	,	SecurityId				INT		NOT NULL
)
GO
