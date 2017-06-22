IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[NotificationPublisherXEventType]')
	AND		name	= N'UQ_NotificationPublisherXEventType_ApplicationId_NotificationPublisherId_NotificationEventTypeId'
)
BEGIN
	PRINT	'Dropping UQ_NotificationPublisherXEventType_ApplicationId_NotificationPublisherId_NotificationEventTypeId'
	ALTER	TABLE dbo.NotificationPublisherXEventType
	DROP	CONSTRAINT	UQ_NotificationPublisherXEventType_ApplicationId_NotificationPublisherId_NotificationEventTypeId
END
GO

ALTER TABLE dbo.NotificationPublisherXEventType
ADD CONSTRAINT UQ_NotificationPublisherXEventType_ApplicationId_NotificationPublisherId_NotificationEventTypeId UNIQUE NONCLUSTERED
(
		ApplicationId
	,	NotificationPublisherId
	,	NotificationEventTypeId
	)
GO
