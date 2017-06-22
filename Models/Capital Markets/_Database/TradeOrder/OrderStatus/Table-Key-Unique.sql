IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].OrderStatus')
	AND		name	= N'UQ_OrderStatus_ApplicationId_OrderStatusTypeId'
)
BEGIN
	PRINT	'Dropping UQ_OrderStatus_ApplicationId_OrderStatusTypeId'
	ALTER	TABLE dbo.OrderStatus
	DROP	CONSTRAINT	UQ_OrderStatus_ApplicationId_OrderStatusTypeId
END
GO

ALTER TABLE dbo.OrderStatus
ADD CONSTRAINT UQ_OrderStatus_ApplicationId_OrderStatusTypeId UNIQUE NONCLUSTERED
(
	ApplicationId, OrderStatusTypeId
)
GO
