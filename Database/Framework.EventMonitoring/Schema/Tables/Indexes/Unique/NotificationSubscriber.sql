IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[NotificationSubscriber]')
	AND		name	= N'UQ_NotificationSubscriber_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_NotificationSubscriber_ApplicationId_Name'
	ALTER	TABLE dbo.NotificationSubscriber
	DROP	CONSTRAINT	UQ_NotificationSubscriber_ApplicationId_Name
END
GO

ALTER TABLE dbo.NotificationSubscriber
ADD CONSTRAINT UQ_NotificationSubscriber_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId,
	Name
)
GO
