IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].OrderStatusType')
	AND		name	= N'UQ_OrderStatusType_ApplicationId_OrderStatusGroupId'
)
BEGIN
	PRINT	'Dropping UQ_OrderStatusType_ApplicationId_OrderStatusGroupId'
	ALTER	TABLE dbo.OrderStatusType
	DROP	CONSTRAINT	UQ_OrderStatusType_ApplicationId_OrderStatusGroupId
END
GO

ALTER TABLE dbo.OrderStatusType
ADD CONSTRAINT UQ_OrderStatusType_ApplicationId_OrderStatusGroupId UNIQUE NONCLUSTERED
(
	ApplicationId, OrderStatusGroupId
)
GO
