IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TxOther')
	AND		name	= N'UQ_TxOther_ApplicationId_TransactionEventId'
)
BEGIN
	PRINT	'Dropping UQ_TxOther_ApplicationId_TransactionEventId'
	ALTER	TABLE dbo.TxOther
	DROP	CONSTRAINT	UQ_TxOther_ApplicationId_TransactionEventId
END
GO

ALTER TABLE dbo.TxOther
ADD CONSTRAINT UQ_TxOther_ApplicationId_TransactionEventId UNIQUE NONCLUSTERED
(
	ApplicationId, TransactionEventId
)
GO
