IF OBJECT_ID ('dbo.MSPAFileEventType') IS NOT NULL
	DROP TABLE dbo.MSPAFileEventType
GO

CREATE TABLE dbo.MSPAFileEventType
(
		MSPAFileEventTypeId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder				INT		NOT NULL
)
GO
