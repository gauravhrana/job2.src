ALTER TABLE dbo.ApplicationMonitoredEvent
	ADD CONSTRAINT FK_ApplicationMonitoredEvent_ApplicationMonitoredEventSource FOREIGN KEY
	(
		ApplicationMonitoredEventSourceId
	)
	REFERENCES ApplicationMonitoredEventSource
	(
		ApplicationMonitoredEventSourceId 
	)
GO

ALTER TABLE dbo.ApplicationMonitoredEvent
	ADD CONSTRAINT FK_ApplicationMonitoredEvent_ApplicationMonitoredEventProcessingState FOREIGN KEY
	(
		ApplicationMonitoredEventProcessingStateId
	)
	REFERENCES ApplicationMonitoredEventProcessingState
	(
		ApplicationMonitoredEventProcessingStateId 
	)
GO