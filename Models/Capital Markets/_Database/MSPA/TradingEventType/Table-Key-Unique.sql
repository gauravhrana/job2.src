IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TradingEventType')
	AND		name	= N'UQ_TradingEventType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TradingEventType_ApplicationId_Name'
	ALTER	TABLE dbo.TradingEventType
	DROP	CONSTRAINT	UQ_TradingEventType_ApplicationId_Name
END
GO

ALTER TABLE dbo.TradingEventType
ADD CONSTRAINT UQ_TradingEventType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
