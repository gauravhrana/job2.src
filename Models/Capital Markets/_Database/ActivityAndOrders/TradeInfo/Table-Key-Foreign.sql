
ALTER TABLE dbo.TxTradeInfo
	ADD CONSTRAINT FK_TxTradeInfo_TransactionEvent FOREIGN KEY
	(
		TransactionEventId
	)
	REFERENCES dbo.TransactionEvent
	(
		TransactionEventId
	)
GO









