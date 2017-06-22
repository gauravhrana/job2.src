IF OBJECT_ID ('dbo.TxOther') IS NOT NULL
	DROP TABLE dbo.TxOther
GO

CREATE TABLE dbo.TxOther
(
		TxOtherId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	TransactionEventId				INT		NOT NULL
	,	FundStructureId				INT		NOT NULL
	,	CashSourceId				INT		NOT NULL
	,	StrategyId				INT		NOT NULL
	,	GenericLegId				INT		NOT NULL
	,	DistributionParentId				INT		NOT NULL
	,	SettlementTypeId				INT		NOT NULL
)
GO
