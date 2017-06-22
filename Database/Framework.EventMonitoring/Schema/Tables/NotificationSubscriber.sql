IF OBJECT_ID ('dbo.NotificationSubscriber') IS NOT NULL
	DROP TABLE dbo.NotificationSubscriber
GO

CREATE TABLE dbo.NotificationSubscriber 
(
		NotificationSubscriberId			INT				NOT NULL
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (100)	NOT NULL	
   	,	SortOrder			INT				NOT NULL
);

GO