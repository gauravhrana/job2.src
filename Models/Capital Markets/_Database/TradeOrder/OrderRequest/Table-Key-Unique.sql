IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].OrderRequest')
	AND		name	= N'UQ_OrderRequest_ApplicationId_PortfolioId'
)
BEGIN
	PRINT	'Dropping UQ_OrderRequest_ApplicationId_PortfolioId'
	ALTER	TABLE dbo.OrderRequest
	DROP	CONSTRAINT	UQ_OrderRequest_ApplicationId_PortfolioId
END
GO

ALTER TABLE dbo.OrderRequest
ADD CONSTRAINT UQ_OrderRequest_ApplicationId_PortfolioId UNIQUE NONCLUSTERED
(
	ApplicationId, PortfolioId
)
GO
