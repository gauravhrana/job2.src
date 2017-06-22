



ALTER TABLE dbo.MSPAFileEvent
	ADD CONSTRAINT FK_MSPAFileEvent_MSPAFile FOREIGN KEY
	(
		MSPAFileId
	)
	REFERENCES dbo.MSPAFile
	(
		MSPAFileId
	)
GO


ALTER TABLE dbo.MSPAFileEvent
	ADD CONSTRAINT FK_MSPAFileEvent_TradingEventType FOREIGN KEY
	(
		TradingEventTypeId
	)
	REFERENCES dbo.TradingEventType
	(
		TradingEventTypeId
	)
GO




