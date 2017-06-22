IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ApplicationMonitoredEventSource_Description'
)

ALTER TABLE dbo.ApplicationMonitoredEventSource
	ADD CONSTRAINT DF_ApplicationMonitoredEventSource_Description		DEFAULT		'' 		FOR Description
GO
