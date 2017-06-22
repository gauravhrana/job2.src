IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SecurityXParty')
	AND		name	= N'UQ_SecurityXParty_ApplicationId_ExchangeId_IssuerId_DeliveryAgentId_SecurityId'
)
BEGIN
	PRINT	'Dropping UQ_SecurityXParty_ApplicationId_ExchangeId_IssuerId_DeliveryAgentId_SecurityId'
	ALTER	TABLE dbo.SecurityXParty
	DROP	CONSTRAINT	UQ_SecurityXParty_ApplicationId_ExchangeId_IssuerId_DeliveryAgentId_SecurityId
END
GO

ALTER TABLE dbo.SecurityXParty
ADD CONSTRAINT UQ_SecurityXParty_ApplicationId_ExchangeId_IssuerId_DeliveryAgentId_SecurityId UNIQUE NONCLUSTERED
(
	ApplicationId, ExchangeId, IssuerId, DeliveryAgentId, SecurityId
)
GO
