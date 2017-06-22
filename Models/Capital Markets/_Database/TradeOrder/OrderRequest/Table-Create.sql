IF OBJECT_ID ('dbo.OrderRequest') IS NOT NULL
	DROP TABLE dbo.OrderRequest
GO

CREATE TABLE dbo.OrderRequest
(
		OrderRequestId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	EventDate				DATETIME		NULL
	,	Notes				VARCHAR(100)		NOT NULL
	,	LastModifiedBy				VARCHAR(100)		NOT NULL
	,	LastModifiedOn				DATETIME		NULL
	,	ParentOrderRequestId				INT		NOT NULL
	,	PortfolioId				INT		NOT NULL
)
GO
