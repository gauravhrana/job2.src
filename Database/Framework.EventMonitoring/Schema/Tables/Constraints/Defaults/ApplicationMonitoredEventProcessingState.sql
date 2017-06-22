IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ApplicationMonitoredEventProcessingState_Description'
)

ALTER TABLE dbo.ApplicationMonitoredEventProcessingState
	ADD CONSTRAINT DF_ApplicationMonitoredEventProcessingState_Description		DEFAULT		'' 		FOR Description
GO
