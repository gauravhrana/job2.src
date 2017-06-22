IF OBJECT_ID ('dbo.OrderStatusGroup') IS NOT NULL
	DROP TABLE dbo.OrderStatusGroup
GO

CREATE TABLE dbo.OrderStatusGroup
(
		OrderStatusGroupId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	OrderStatusGroupCode				VARCHAR(100)		NOT NULL
	,	OrderStatusGroupDescription				VARCHAR(100)		NOT NULL
	,	OrderProcessFlag				INT		NOT NULL
)
GO
