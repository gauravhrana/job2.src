IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TxTradeAndSettleDates')
	AND		name	= N'UQ_TxTradeAndSettleDates_ApplicationId_TransactionEventId'
)
BEGIN
	PRINT	'Dropping UQ_TxTradeAndSettleDates_ApplicationId_TransactionEventId'
	ALTER	TABLE dbo.TxTradeAndSettleDates
	DROP	CONSTRAINT	UQ_TxTradeAndSettleDates_ApplicationId_TransactionEventId
END
GO

ALTER TABLE dbo.TxTradeAndSettleDates
ADD CONSTRAINT UQ_TxTradeAndSettleDates_ApplicationId_TransactionEventId UNIQUE NONCLUSTERED
(
	ApplicationId, TransactionEventId
)
GO
