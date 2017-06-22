
IF OBJECT_ID ('dbo.NotificationPublisherXNotificationEventType ') IS NOT NULL
	DROP TABLE dbo.NotificationPublisherXNotificationEventType 
GO
CREATE TABLE dbo.NotificationPublisherXNotificationEventType 
(	NotificationPublisherXNotificationEventTypeId	INT NOT NULL,
	ApplicationId		INT NOT NULL,
	NotificationPublisherId			INT NOT NULL,
	NotificationEventTypeId			INT NOT NULL,
	CreatedDateId					DECIMAL(15,0)

) 

GO


