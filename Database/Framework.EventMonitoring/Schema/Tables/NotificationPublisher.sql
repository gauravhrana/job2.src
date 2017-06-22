IF OBJECT_ID ('dbo.NotificationPublisher') IS NOT NULL
	DROP TABLE dbo.NotificationPublisher
GO

CREATE TABLE dbo.NotificationPublisher 
(
		NotificationPublisherId			INT				NOT NULL
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (100)	NOT NULL	
   	,	SortOrder			INT				NOT NULL
);

GO