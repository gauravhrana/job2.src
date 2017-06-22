IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ApplicationMonitoredEventProcessingState]')
	AND		name	= N'UQ_ApplicationMonitoredEventProcessingState_Code'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationMonitoredEventProcessingState_Code'
	ALTER	TABLE dbo.ApplicationMonitoredEventProcessingState
	DROP	CONSTRAINT	UQ_ApplicationMonitoredEventProcessingState_Code
END
GO

ALTER TABLE dbo.ApplicationMonitoredEventProcessingState
ADD CONSTRAINT UQ_ApplicationMonitoredEventProcessingState_Code UNIQUE NONCLUSTERED
(
	Code
)
GO
