





ALTER TABLE dbo.OrderRequest
	ADD CONSTRAINT FK_OrderRequest_Portfolio FOREIGN KEY
	(
		PortfolioId
	)
	REFERENCES dbo.Portfolio
	(
		PortfolioId
	)
GO




