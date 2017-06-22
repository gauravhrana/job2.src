IF OBJECT_ID ('dbo.TradingEventType') IS NOT NULL
	DROP TABLE dbo.TradingEventType
GO

CREATE TABLE dbo.TradingEventType
(
		TradingEventTypeId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder				INT		NOT NULL
)
GO
