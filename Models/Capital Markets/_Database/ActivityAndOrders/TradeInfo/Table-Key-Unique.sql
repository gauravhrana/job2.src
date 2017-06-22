IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TxTradeInfo')
	AND		name	= N'UQ_TxTradeInfo_ApplicationId_TransactionEventId'
)
BEGIN
	PRINT	'Dropping UQ_TxTradeInfo_ApplicationId_TransactionEventId'
	ALTER	TABLE dbo.TxTradeInfo
	DROP	CONSTRAINT	UQ_TxTradeInfo_ApplicationId_TransactionEventId
END
GO

ALTER TABLE dbo.TxTradeInfo
ADD CONSTRAINT UQ_TxTradeInfo_ApplicationId_TransactionEventId UNIQUE NONCLUSTERED
(
	ApplicationId, TransactionEventId
)
GO
