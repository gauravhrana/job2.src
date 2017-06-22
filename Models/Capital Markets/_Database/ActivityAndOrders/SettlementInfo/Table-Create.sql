IF OBJECT_ID ('dbo.TxSettlementInfo') IS NOT NULL
	DROP TABLE dbo.TxSettlementInfo
GO

CREATE TABLE dbo.TxSettlementInfo
(
		TxSettlementInfoId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	TransactionEventId				INT		NOT NULL
	,	SettlementCurrencyId				INT		NOT NULL
	,	SellCurrencyId				INT		NOT NULL
	,	TradeDateFXRate				VARCHAR(100)		NOT NULL
	,	NetSettlementAmount				VARCHAR(100)		NOT NULL
	,	NetSettlementCashAmount				VARCHAR(100)		NOT NULL
	,	AccruedInterest				VARCHAR(100)		NOT NULL
)
GO
