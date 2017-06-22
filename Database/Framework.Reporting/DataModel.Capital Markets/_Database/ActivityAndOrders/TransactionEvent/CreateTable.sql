IF OBJECT_ID ('dbo.TransactionEvent') IS NOT NULL
	DROP TABLE dbo.TransactionEvent
GO

CREATE TABLE dbo.TransactionEvent
(
		TransactionEventId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	TransactionEventDate				DATETIME		NOT NULL
	,	TransactionSettleDate				DATETIME		NOT NULL
	,	TransactionTypeCode				VARCHAR(100)		NOT NULL
	,	CustodianCode				VARCHAR(100)		NOT NULL
	,	StrategyCode				VARCHAR(100)		NOT NULL
	,	AccountCode				VARCHAR(100)		NOT NULL
	,	InvestmentCode				VARCHAR(100)		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
