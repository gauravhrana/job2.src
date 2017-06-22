IF OBJECT_ID ('dbo.OrderType') IS NOT NULL
	DROP TABLE dbo.OrderType
GO

CREATE TABLE dbo.OrderType
(
		OrderTypeId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Code				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
)
GO
