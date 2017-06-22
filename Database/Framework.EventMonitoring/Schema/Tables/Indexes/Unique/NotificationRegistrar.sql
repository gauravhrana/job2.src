IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[NotificationRegistrar]')
	AND		name	= N'UQ_NotificationRegistrar_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_NotificationRegistrar_ApplicationId'
	ALTER	TABLE dbo.NotificationRegistrar
	DROP	CONSTRAINT	UQ_NotificationRegistrar_ApplicationId
END
GO

ALTER TABLE dbo.NotificationRegistrar
ADD CONSTRAINT UQ_NotificationRegistrar_ApplicationId UNIQUE NONCLUSTERED
(
		ApplicationId
	
	
)
GO
