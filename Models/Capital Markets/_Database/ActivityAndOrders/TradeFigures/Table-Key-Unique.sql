IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TxTradeFigures')
	AND		name	= N'UQ_TxTradeFigures_ApplicationId_TransactionEventId'
)
BEGIN
	PRINT	'Dropping UQ_TxTradeFigures_ApplicationId_TransactionEventId'
	ALTER	TABLE dbo.TxTradeFigures
	DROP	CONSTRAINT	UQ_TxTradeFigures_ApplicationId_TransactionEventId
END
GO

ALTER TABLE dbo.TxTradeFigures
ADD CONSTRAINT UQ_TxTradeFigures_ApplicationId_TransactionEventId UNIQUE NONCLUSTERED
(
	ApplicationId, TransactionEventId
)
GO
