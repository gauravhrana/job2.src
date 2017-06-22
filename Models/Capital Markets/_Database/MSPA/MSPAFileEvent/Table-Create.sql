IF OBJECT_ID ('dbo.MSPAFileEvent') IS NOT NULL
	DROP TABLE dbo.MSPAFileEvent
GO

CREATE TABLE dbo.MSPAFileEvent
(
		MSPAFileEventId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	CreatedBy				VARCHAR(100)		NOT NULL
	,	CreatedOn				DATETIME		NULL
	,	MSPAFileId				INT		NOT NULL
	,	TradingEventTypeId				INT		NOT NULL
)
GO
