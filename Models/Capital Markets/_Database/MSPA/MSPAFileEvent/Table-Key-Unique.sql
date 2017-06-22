IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].MSPAFileEvent')
	AND		name	= N'UQ_MSPAFileEvent_ApplicationId_MSPAFileId_TradingEventTypeId'
)
BEGIN
	PRINT	'Dropping UQ_MSPAFileEvent_ApplicationId_MSPAFileId_TradingEventTypeId'
	ALTER	TABLE dbo.MSPAFileEvent
	DROP	CONSTRAINT	UQ_MSPAFileEvent_ApplicationId_MSPAFileId_TradingEventTypeId
END
GO

ALTER TABLE dbo.MSPAFileEvent
ADD CONSTRAINT UQ_MSPAFileEvent_ApplicationId_MSPAFileId_TradingEventTypeId UNIQUE NONCLUSTERED
(
	ApplicationId, MSPAFileId, TradingEventTypeId
)
GO
