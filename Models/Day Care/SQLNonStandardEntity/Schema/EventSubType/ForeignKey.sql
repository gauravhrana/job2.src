
ALTER TABLE dbo.EventSubType
	ADD CONSTRAINT FK_EventSubType_EventType FOREIGN KEY
	(
		EventTypeId
	)
	REFERENCES dbo.EventType
	(
		EventTypeId
	)
GO









