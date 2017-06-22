IF OBJECT_ID ('dbo.TimeZone') IS NOT NULL
	DROP TABLE dbo.TimeZone
GO

CREATE TABLE dbo.TimeZone
(
		TimeZoneId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	TimeDifference				DECIMAL(18, 5)		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder				INT		NOT NULL
)
GO
