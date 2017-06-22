
ALTER TABLE dbo.TxOther
	ADD CONSTRAINT FK_TxOther_TransactionEvent FOREIGN KEY
	(
		TransactionEventId
	)
	REFERENCES dbo.TransactionEvent
	(
		TransactionEventId
	)
GO










