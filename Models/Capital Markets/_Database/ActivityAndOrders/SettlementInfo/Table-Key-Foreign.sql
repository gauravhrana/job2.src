
ALTER TABLE dbo.TxSettlementInfo
	ADD CONSTRAINT FK_TxSettlementInfo_TransactionEvent FOREIGN KEY
	(
		TransactionEventId
	)
	REFERENCES dbo.TransactionEvent
	(
		TransactionEventId
	)
GO










