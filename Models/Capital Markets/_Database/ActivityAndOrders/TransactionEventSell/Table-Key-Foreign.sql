


ALTER TABLE dbo.TransactionEventSell
	ADD CONSTRAINT FK_TransactionEventSell_TransactionType FOREIGN KEY
	(
		TransactionTypeId
	)
	REFERENCES dbo.TransactionType
	(
		TransactionTypeId
	)
GO


ALTER TABLE dbo.TransactionEventSell
	ADD CONSTRAINT FK_TransactionEventSell_Custodian FOREIGN KEY
	(
		CustodianId
	)
	REFERENCES dbo.Custodian
	(
		CustodianId
	)
GO


ALTER TABLE dbo.TransactionEventSell
	ADD CONSTRAINT FK_TransactionEventSell_Strategy FOREIGN KEY
	(
		StrategyId
	)
	REFERENCES dbo.Strategy
	(
		StrategyId
	)
GO


ALTER TABLE dbo.TransactionEventSell
	ADD CONSTRAINT FK_TransactionEventSell_AccountSpecificType FOREIGN KEY
	(
		AccountSpecificTypeId
	)
	REFERENCES dbo.AccountSpecificType
	(
		AccountSpecificTypeId
	)
GO


ALTER TABLE dbo.TransactionEventSell
	ADD CONSTRAINT FK_TransactionEventSell_InvestmentType FOREIGN KEY
	(
		InvestmentTypeId
	)
	REFERENCES dbo.InvestmentType
	(
		InvestmentTypeId
	)
GO







