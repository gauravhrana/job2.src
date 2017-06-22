


ALTER TABLE dbo.TransactionEventSellShort
	ADD CONSTRAINT FK_TransactionEventSellShort_TransactionType FOREIGN KEY
	(
		TransactionTypeId
	)
	REFERENCES dbo.TransactionType
	(
		TransactionTypeId
	)
GO


ALTER TABLE dbo.TransactionEventSellShort
	ADD CONSTRAINT FK_TransactionEventSellShort_Custodian FOREIGN KEY
	(
		CustodianId
	)
	REFERENCES dbo.Custodian
	(
		CustodianId
	)
GO


ALTER TABLE dbo.TransactionEventSellShort
	ADD CONSTRAINT FK_TransactionEventSellShort_Strategy FOREIGN KEY
	(
		StrategyId
	)
	REFERENCES dbo.Strategy
	(
		StrategyId
	)
GO


ALTER TABLE dbo.TransactionEventSellShort
	ADD CONSTRAINT FK_TransactionEventSellShort_AccountSpecificType FOREIGN KEY
	(
		AccountSpecificTypeId
	)
	REFERENCES dbo.AccountSpecificType
	(
		AccountSpecificTypeId
	)
GO


ALTER TABLE dbo.TransactionEventSellShort
	ADD CONSTRAINT FK_TransactionEventSellShort_InvestmentType FOREIGN KEY
	(
		InvestmentTypeId
	)
	REFERENCES dbo.InvestmentType
	(
		InvestmentTypeId
	)
GO







