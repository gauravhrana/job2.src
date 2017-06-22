IF OBJECT_ID ('dbo.SecurityXSettlementDay') IS NOT NULL
	DROP TABLE dbo.SecurityXSettlementDay
GO

CREATE TABLE dbo.SecurityXSettlementDay
(
		SecurityXSettlementDayId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	SettlementDay				INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder				INT		NOT NULL
)
GO
