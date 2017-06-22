IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[NotificationPublisher]')
	AND		name	= N'UQ_NotificationPublisher_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_NotificationPublisher_ApplicationId_Name'
	ALTER	TABLE dbo.NotificationPublisher
	DROP	CONSTRAINT	UQ_NotificationPublisher_ApplicationId_Name
END
GO

ALTER TABLE dbo.NotificationPublisher
ADD CONSTRAINT UQ_NotificationPublisher_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId,
	Name
)
GO
