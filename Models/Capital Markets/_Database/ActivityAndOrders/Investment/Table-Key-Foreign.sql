
ALTER TABLE dbo.TxInvestment
	ADD CONSTRAINT FK_TxInvestment_TransactionEvent FOREIGN KEY
	(
		TransactionEventId
	)
	REFERENCES dbo.TransactionEvent
	(
		TransactionEventId
	)
GO






