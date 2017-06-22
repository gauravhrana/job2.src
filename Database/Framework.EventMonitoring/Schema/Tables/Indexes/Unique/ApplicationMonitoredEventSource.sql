IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ApplicationMonitoredEventSource]')
	AND		name	= N'UQ_ApplicationMonitoredEventSource_Code'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationMonitoredEventSource_Code'
	ALTER	TABLE dbo.ApplicationMonitoredEventSource
	DROP	CONSTRAINT	UQ_ApplicationMonitoredEventSource_Code
END
GO

ALTER TABLE dbo.ApplicationMonitoredEventSource
ADD CONSTRAINT UQ_ApplicationMonitoredEventSource_Code UNIQUE NONCLUSTERED
(
	Code
)
GO
