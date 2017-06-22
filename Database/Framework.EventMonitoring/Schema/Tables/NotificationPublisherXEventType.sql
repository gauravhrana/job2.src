
IF OBJECT_ID ('dbo.NotificationPublisherXEventType ') IS NOT NULL
	DROP TABLE dbo.NotificationPublisherXEventType 
GO
CREATE TABLE dbo.NotificationPublisherXEventType 
(	NotificationPublisherXEventTypeId	INT NOT NULL,
	ApplicationId		INT NOT NULL,
	NotificationPublisherId			INT NOT NULL,
	NotificationEventTypeId			INT NOT NULL,
	CreatedDateId					INT NOT NULL,
	CreatedTimeId					INT NOT NULL
) 

GO


