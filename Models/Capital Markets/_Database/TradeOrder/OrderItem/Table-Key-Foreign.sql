






























ALTER TABLE dbo.OrderItem
	ADD CONSTRAINT FK_OrderItem_OrderRequest FOREIGN KEY
	(
		OrderRequestId
	)
	REFERENCES dbo.OrderRequest
	(
		OrderRequestId
	)
GO


ALTER TABLE dbo.OrderItem
	ADD CONSTRAINT FK_OrderItem_OrderAction FOREIGN KEY
	(
		OrderActionId
	)
	REFERENCES dbo.OrderAction
	(
		OrderActionId
	)
GO


ALTER TABLE dbo.OrderItem
	ADD CONSTRAINT FK_OrderItem_OrderType FOREIGN KEY
	(
		OrderTypeId
	)
	REFERENCES dbo.OrderType
	(
		OrderTypeId
	)
GO


ALTER TABLE dbo.OrderItem
	ADD CONSTRAINT FK_OrderItem_Strategy FOREIGN KEY
	(
		StrategyId
	)
	REFERENCES dbo.Strategy
	(
		StrategyId
	)
GO


ALTER TABLE dbo.OrderItem
	ADD CONSTRAINT FK_OrderItem_Security FOREIGN KEY
	(
		SecurityId
	)
	REFERENCES dbo.Security
	(
		SecurityId
	)
GO


ALTER TABLE dbo.OrderItem
	ADD CONSTRAINT FK_OrderItem_Portfolio FOREIGN KEY
	(
		PortfolioId
	)
	REFERENCES dbo.Portfolio
	(
		PortfolioId
	)
GO




