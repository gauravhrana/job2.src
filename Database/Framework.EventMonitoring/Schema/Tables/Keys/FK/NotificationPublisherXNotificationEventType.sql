ALTER TABLE dbo.NotificationPublisherXNotificationEventType
	ADD CONSTRAINT FK_NotificationPublisherXNotificationEventType_NotificationEventType FOREIGN KEY
	(
		NotificationEventTypeId
	)
	REFERENCES NotificationEventType
	(
		NotificationEventTypeId
	)
GO

ALTER TABLE dbo.NotificationPublisherXNotificationEventType
	ADD CONSTRAINT FK_NotificationPublisherXNotificationEventType_NotificationPublisher FOREIGN KEY
	(
		NotificationPublisherId
	)
	REFERENCES dbo.NotificationPublisher
	(
		NotificationPublisherId
	)
GO