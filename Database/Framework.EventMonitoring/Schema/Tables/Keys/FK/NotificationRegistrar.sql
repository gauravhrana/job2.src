ALTER TABLE dbo.NotificationRegistrar
	ADD CONSTRAINT FK_NotificationRegistrar_NotificationEventType FOREIGN KEY
	(
		NotificationEventTypeId
	)
	REFERENCES NotificationEventType
	(
		NotificationEventTypeId 
	)
GO

ALTER TABLE dbo.NotificationRegistrar
	ADD CONSTRAINT FK_NotificationRegistrar_NotificationPublisher FOREIGN KEY
	(
		NotificationPublisherId
	)
	REFERENCES dbo.NotificationPublisher
	(
		NotificationPublisherId
	)
GO