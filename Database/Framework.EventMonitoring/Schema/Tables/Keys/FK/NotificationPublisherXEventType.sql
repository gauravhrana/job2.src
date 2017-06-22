ALTER TABLE dbo.NotificationPublisherXEventType
	ADD CONSTRAINT FK_NotificationPublisherXEventType_NotificationEventType FOREIGN KEY
	(
		NotificationEventTypeId
	)
	REFERENCES NotificationEventType
	(
		NotificationEventTypeId
	)
GO

ALTER TABLE dbo.NotificationPublisherXEventType
	ADD CONSTRAINT FK_NotificationPublisherXEventType_NotificationPublisher FOREIGN KEY
	(
		NotificationPublisherId
	)
	REFERENCES dbo.NotificationPublisher
	(
		NotificationPublisherId
	)
GO