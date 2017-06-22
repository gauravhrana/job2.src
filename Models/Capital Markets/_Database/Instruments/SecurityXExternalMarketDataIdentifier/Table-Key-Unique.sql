IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SecurityXExternalMarketDataIdentifier')
	AND		name	= N'UQ_SecurityXExternalMarketDataIdentifier_ApplicationId_SecurityId'
)
BEGIN
	PRINT	'Dropping UQ_SecurityXExternalMarketDataIdentifier_ApplicationId_SecurityId'
	ALTER	TABLE dbo.SecurityXExternalMarketDataIdentifier
	DROP	CONSTRAINT	UQ_SecurityXExternalMarketDataIdentifier_ApplicationId_SecurityId
END
GO

ALTER TABLE dbo.SecurityXExternalMarketDataIdentifier
ADD CONSTRAINT UQ_SecurityXExternalMarketDataIdentifier_ApplicationId_SecurityId UNIQUE NONCLUSTERED
(
	ApplicationId, SecurityId
)
GO
