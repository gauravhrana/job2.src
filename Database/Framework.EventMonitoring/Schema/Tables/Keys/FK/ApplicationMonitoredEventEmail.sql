ALTER TABLE dbo.ApplicationMonitoredEventEmail
	ADD CONSTRAINT FK_ApplicationMonitoredEventEmail_ApplicationMonitoredEventSource FOREIGN KEY
	(
		ApplicationMonitoredEventSourceId
	)
	REFERENCES ApplicationMonitoredEventSource
	(
		ApplicationMonitoredEventSourceId 
	)
GO

ALTER TABLE dbo.ApplicationMonitoredEventEmail
	ADD CONSTRAINT FK_ApplicationMonitoredEventEmail_Person FOREIGN KEY
	(
		UserId
	)
	REFERENCES Person
	(
		PersonId 
	)
GO