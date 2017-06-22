IF OBJECT_ID ('dbo.OrderStatus') IS NOT NULL
	DROP TABLE dbo.OrderStatus
GO

CREATE TABLE dbo.OrderStatus
(
		OrderStatusId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	OrderId				INT		NOT NULL
	,	Comments				VARCHAR(100)		NOT NULL
	,	LastModifiedBy				VARCHAR(100)		NOT NULL
	,	LastModifiedOn				DATETIME		NULL
	,	OrderStatusTypeId				INT		NOT NULL
)
GO
