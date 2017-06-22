IF OBJECT_ID ('dbo.NotificationRegistrar') IS NOT NULL
	DROP TABLE dbo.NotificationRegistrar
GO

CREATE TABLE dbo.NotificationRegistrar 
(
		NotificationRegistrarId			INT				NOT NULL
	,	ApplicationId					INT				NOT NULL
    ,	NotificationEventTypeId			INT				NOT NULL
	,	NotificationPublisherId			INT				NOT NULL		
	,	Message						VARCHAR (255)	NOT NULL	
   	,	PublishDateId				INT				NOT NULL
	,	PublishTimeId				INT				NOT NULL
);

GO