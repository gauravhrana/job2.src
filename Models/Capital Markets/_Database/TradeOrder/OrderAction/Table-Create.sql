IF OBJECT_ID ('dbo.OrderAction') IS NOT NULL
	DROP TABLE dbo.OrderAction
GO

CREATE TABLE dbo.OrderAction
(
		OrderActionId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	OrderActionCode				VARCHAR(100)		NOT NULL
	,	OrderActionDescription				VARCHAR(100)		NOT NULL
	,	PositionDirection				VARCHAR(100)		NOT NULL
)
GO
