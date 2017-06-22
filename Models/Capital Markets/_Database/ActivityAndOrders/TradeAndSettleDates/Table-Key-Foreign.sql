
ALTER TABLE dbo.TxTradeAndSettleDates
	ADD CONSTRAINT FK_TxTradeAndSettleDates_TransactionEvent FOREIGN KEY
	(
		TransactionEventId
	)
	REFERENCES dbo.TransactionEvent
	(
		TransactionEventId
	)
GO



















