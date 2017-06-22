IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].InvestmentPrices')
	AND		name	= N'UQ_InvestmentPrices_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_InvestmentPrices_ApplicationId_Name'
	ALTER	TABLE dbo.InvestmentPrices
	DROP	CONSTRAINT	UQ_InvestmentPrices_ApplicationId_Name
END
GO

ALTER TABLE dbo.InvestmentPrices
ADD CONSTRAINT UQ_InvestmentPrices_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
