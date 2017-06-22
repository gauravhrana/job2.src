IF OBJECT_ID ('dbo.SecurityXExternalMarketDataIdentifier') IS NOT NULL
	DROP TABLE dbo.SecurityXExternalMarketDataIdentifier
GO

CREATE TABLE dbo.SecurityXExternalMarketDataIdentifier
(
		SecurityXExternalMarketDataIdentifierId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	BloombergGlobalId				INT		NOT NULL
	,	BloombergTicker				VARCHAR(100)		NOT NULL
	,	BloombergUniqueId				INT		NOT NULL
	,	BloombergMarketSector				VARCHAR(100)		NOT NULL
	,	RICCode				VARCHAR(100)		NOT NULL
	,	IDCCode				VARCHAR(100)		NOT NULL
	,	RedCode				VARCHAR(100)		NOT NULL
	,	PriceWithSuperDerivatives				VARCHAR(100)		NOT NULL
	,	SecurityId				INT		NOT NULL
)
GO
