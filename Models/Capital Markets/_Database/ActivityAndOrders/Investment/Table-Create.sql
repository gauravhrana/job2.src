IF OBJECT_ID ('dbo.TxInvestment') IS NOT NULL
	DROP TABLE dbo.TxInvestment
GO

CREATE TABLE dbo.TxInvestment
(
		TxInvestmentId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	TransactionEventId				INT		NOT NULL
	,	InvestmentId				INT		NOT NULL
	,	CustAccountId				INT		NOT NULL
)
GO
