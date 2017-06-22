IF OBJECT_ID ('dbo.TxTradeInfo') IS NOT NULL
	DROP TABLE dbo.TxTradeInfo
GO

CREATE TABLE dbo.TxTradeInfo
(
		TxTradeInfoId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	TransactionEventId				INT		NOT NULL
	,	TradeCurrencyId				INT		NOT NULL
	,	BuyCurrencyId				INT		NOT NULL
	,	CrossSettlementFXRate				VARCHAR(100)		NOT NULL
	,	NetTradeAmount				VARCHAR(100)		NOT NULL
	,	TradeAccruedInterest				VARCHAR(100)		NOT NULL
)
GO
