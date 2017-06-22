


ALTER TABLE dbo.TransactionEvent
	ADD CONSTRAINT FK_TransactionEvent_TransactionType FOREIGN KEY
	(
		TransactionTypeId
	)
	REFERENCES dbo.TransactionType
	(
		TransactionTypeId
	)
GO


ALTER TABLE dbo.TransactionEvent
	ADD CONSTRAINT FK_TransactionEvent_Custodian FOREIGN KEY
	(
		CustodianId
	)
	REFERENCES dbo.Custodian
	(
		CustodianId
	)
GO


ALTER TABLE dbo.TransactionEvent
	ADD CONSTRAINT FK_TransactionEvent_Strategy FOREIGN KEY
	(
		StrategyId
	)
	REFERENCES dbo.Strategy
	(
		StrategyId
	)
GO


ALTER TABLE dbo.TransactionEvent
	ADD CONSTRAINT FK_TransactionEvent_AccountSpecificType FOREIGN KEY
	(
		AccountSpecificTypeId
	)
	REFERENCES dbo.AccountSpecificType
	(
		AccountSpecificTypeId
	)
GO


ALTER TABLE dbo.TransactionEvent
	ADD CONSTRAINT FK_TransactionEvent_InvestmentType FOREIGN KEY
	(
		InvestmentTypeId
	)
	REFERENCES dbo.InvestmentType
	(
		InvestmentTypeId
	)
GO







