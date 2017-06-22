IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CommissionRate')
	AND		name	= N'UQ_CommissionRate_ApplicationId_BrokerId_ExchangeId'
)
BEGIN
	PRINT	'Dropping UQ_CommissionRate_ApplicationId_BrokerId_ExchangeId'
	ALTER	TABLE dbo.CommissionRate
	DROP	CONSTRAINT	UQ_CommissionRate_ApplicationId_BrokerId_ExchangeId
END
GO

ALTER TABLE dbo.CommissionRate
ADD CONSTRAINT UQ_CommissionRate_ApplicationId_BrokerId_ExchangeId UNIQUE NONCLUSTERED
(
	ApplicationId, BrokerId, ExchangeId
)
GO
