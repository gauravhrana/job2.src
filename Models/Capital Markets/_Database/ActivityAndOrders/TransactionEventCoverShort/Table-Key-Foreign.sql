


ALTER TABLE dbo.TransactionEventCoverShort
	ADD CONSTRAINT FK_TransactionEventCoverShort_TransactionType FOREIGN KEY
	(
		TransactionTypeId
	)
	REFERENCES dbo.TransactionType
	(
		TransactionTypeId
	)
GO


ALTER TABLE dbo.TransactionEventCoverShort
	ADD CONSTRAINT FK_TransactionEventCoverShort_Custodian FOREIGN KEY
	(
		CustodianId
	)
	REFERENCES dbo.Custodian
	(
		CustodianId
	)
GO


ALTER TABLE dbo.TransactionEventCoverShort
	ADD CONSTRAINT FK_TransactionEventCoverShort_Strategy FOREIGN KEY
	(
		StrategyId
	)
	REFERENCES dbo.Strategy
	(
		StrategyId
	)
GO


ALTER TABLE dbo.TransactionEventCoverShort
	ADD CONSTRAINT FK_TransactionEventCoverShort_AccountSpecificType FOREIGN KEY
	(
		AccountSpecificTypeId
	)
	REFERENCES dbo.AccountSpecificType
	(
		AccountSpecificTypeId
	)
GO


ALTER TABLE dbo.TransactionEventCoverShort
	ADD CONSTRAINT FK_TransactionEventCoverShort_InvestmentType FOREIGN KEY
	(
		InvestmentTypeId
	)
	REFERENCES dbo.InvestmentType
	(
		InvestmentTypeId
	)
GO







