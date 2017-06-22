IF OBJECT_ID ('dbo.TxTradeAndSettleDates') IS NOT NULL
	DROP TABLE dbo.TxTradeAndSettleDates
GO

CREATE TABLE dbo.TxTradeAndSettleDates
(
		TxTradeAndSettleDatesId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	TransactionEventId				INT		NOT NULL
	,	TradeDate				DATETIME		NULL
	,	ContractualDate				DATETIME		NULL
	,	ActualDate				DATETIME		NULL
	,	SpotDate				DATETIME		NULL
	,	SettlementDate				DATETIME		NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId				INT		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	ModifiedByAuditId				INT		NOT NULL
)
GO
