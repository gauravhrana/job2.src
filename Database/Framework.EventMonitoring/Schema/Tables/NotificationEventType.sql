IF OBJECT_ID ('dbo.NotificationEventType') IS NOT NULL
	DROP TABLE dbo.NotificationEventType
GO

CREATE TABLE dbo.NotificationEventType 
(
		NotificationEventTypeId			INT				NOT NULL
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (100)	NOT NULL	
   	,	SortOrder			INT				NOT NULL
);

GO