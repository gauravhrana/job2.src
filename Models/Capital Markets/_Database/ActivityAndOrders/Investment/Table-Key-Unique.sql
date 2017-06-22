IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TxInvestment')
	AND		name	= N'UQ_TxInvestment_ApplicationId_TransactionEventId'
)
BEGIN
	PRINT	'Dropping UQ_TxInvestment_ApplicationId_TransactionEventId'
	ALTER	TABLE dbo.TxInvestment
	DROP	CONSTRAINT	UQ_TxInvestment_ApplicationId_TransactionEventId
END
GO

ALTER TABLE dbo.TxInvestment
ADD CONSTRAINT UQ_TxInvestment_ApplicationId_TransactionEventId UNIQUE NONCLUSTERED
(
	ApplicationId, TransactionEventId
)
GO
