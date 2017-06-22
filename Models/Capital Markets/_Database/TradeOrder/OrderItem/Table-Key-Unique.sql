IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].OrderItem')
	AND		name	= N'UQ_OrderItem_ApplicationId_OrderRequestId_OrderActionId_OrderTypeId_StrategyId_SecurityId_PortfolioId'
)
BEGIN
	PRINT	'Dropping UQ_OrderItem_ApplicationId_OrderRequestId_OrderActionId_OrderTypeId_StrategyId_SecurityId_PortfolioId'
	ALTER	TABLE dbo.OrderItem
	DROP	CONSTRAINT	UQ_OrderItem_ApplicationId_OrderRequestId_OrderActionId_OrderTypeId_StrategyId_SecurityId_PortfolioId
END
GO

ALTER TABLE dbo.OrderItem
ADD CONSTRAINT UQ_OrderItem_ApplicationId_OrderRequestId_OrderActionId_OrderTypeId_StrategyId_SecurityId_PortfolioId UNIQUE NONCLUSTERED
(
	ApplicationId, OrderRequestId, OrderActionId, OrderTypeId, StrategyId, SecurityId, PortfolioId
)
GO
