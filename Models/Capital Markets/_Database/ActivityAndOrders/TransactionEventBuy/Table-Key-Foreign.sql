


ALTER TABLE dbo.TransactionEventBuy
	ADD CONSTRAINT FK_TransactionEventBuy_TransactionType FOREIGN KEY
	(
		TransactionTypeId
	)
	REFERENCES dbo.TransactionType
	(
		TransactionTypeId
	)
GO


ALTER TABLE dbo.TransactionEventBuy
	ADD CONSTRAINT FK_TransactionEventBuy_Custodian FOREIGN KEY
	(
		CustodianId
	)
	REFERENCES dbo.Custodian
	(
		CustodianId
	)
GO


ALTER TABLE dbo.TransactionEventBuy
	ADD CONSTRAINT FK_TransactionEventBuy_Strategy FOREIGN KEY
	(
		StrategyId
	)
	REFERENCES dbo.Strategy
	(
		StrategyId
	)
GO


ALTER TABLE dbo.TransactionEventBuy
	ADD CONSTRAINT FK_TransactionEventBuy_AccountSpecificType FOREIGN KEY
	(
		AccountSpecificTypeId
	)
	REFERENCES dbo.AccountSpecificType
	(
		AccountSpecificTypeId
	)
GO


ALTER TABLE dbo.TransactionEventBuy
	ADD CONSTRAINT FK_TransactionEventBuy_InvestmentType FOREIGN KEY
	(
		InvestmentTypeId
	)
	REFERENCES dbo.InvestmentType
	(
		InvestmentTypeId
	)
GO







