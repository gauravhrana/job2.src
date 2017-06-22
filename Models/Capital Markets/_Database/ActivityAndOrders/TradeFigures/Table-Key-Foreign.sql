
ALTER TABLE dbo.TxTradeFigures
	ADD CONSTRAINT FK_TxTradeFigures_TransactionEvent FOREIGN KEY
	(
		TransactionEventId
	)
	REFERENCES dbo.TransactionEvent
	(
		TransactionEventId
	)
GO
































