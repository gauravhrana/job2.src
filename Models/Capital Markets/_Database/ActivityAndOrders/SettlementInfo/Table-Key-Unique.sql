IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TxSettlementInfo')
	AND		name	= N'UQ_TxSettlementInfo_ApplicationId_TransactionEventId'
)
BEGIN
	PRINT	'Dropping UQ_TxSettlementInfo_ApplicationId_TransactionEventId'
	ALTER	TABLE dbo.TxSettlementInfo
	DROP	CONSTRAINT	UQ_TxSettlementInfo_ApplicationId_TransactionEventId
END
GO

ALTER TABLE dbo.TxSettlementInfo
ADD CONSTRAINT UQ_TxSettlementInfo_ApplicationId_TransactionEventId UNIQUE NONCLUSTERED
(
	ApplicationId, TransactionEventId
)
GO
