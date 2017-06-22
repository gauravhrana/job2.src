IF OBJECT_ID ('dbo.TransactionEventBuy') IS NOT NULL
	DROP TABLE dbo.TransactionEventBuy
GO

CREATE TABLE dbo.TransactionEventBuy
(
		TransactionEventBuyId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	TransactionEventDate				DATETIME		NOT NULL
	,	TransactionSettleDate				DATETIME		NOT NULL
	,	TransactionTypeId				INT		NOT NULL
	,	CustodianId				INT		NOT NULL
	,	StrategyId				INT		NOT NULL
	,	AccountSpecificTypeId				INT		NOT NULL
	,	InvestmentTypeId				INT		NOT NULL
	,	Quantity				INT		NOT NULL
	,	Price				INT		NOT NULL
	,	Fees				INT		NOT NULL
)
GO