IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[NotificationEventType]')
	AND		name	= N'UQ_NotificationEventType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_NotificationEventType_ApplicationId_Name'
	ALTER	TABLE dbo.NotificationEventType
	DROP	CONSTRAINT	UQ_NotificationEventType_ApplicationId_Name
END
GO

ALTER TABLE dbo.NotificationEventType
ADD CONSTRAINT UQ_NotificationEventType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId,
	Name
)
GO
