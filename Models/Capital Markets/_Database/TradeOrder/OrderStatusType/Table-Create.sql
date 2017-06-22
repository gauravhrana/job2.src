IF OBJECT_ID ('dbo.OrderStatusType') IS NOT NULL
	DROP TABLE dbo.OrderStatusType
GO

CREATE TABLE dbo.OrderStatusType
(
		OrderStatusTypeId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Code				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	OrderStatusGroupId				INT		NOT NULL
)
GO
