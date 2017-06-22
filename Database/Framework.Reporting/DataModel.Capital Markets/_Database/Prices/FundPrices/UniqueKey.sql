IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].FundPrices')
	AND		name	= N'UQ_FundPrices_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_FundPrices_ApplicationId_Name'
	ALTER	TABLE dbo.FundPrices
	DROP	CONSTRAINT	UQ_FundPrices_ApplicationId_Name
END
GO

ALTER TABLE dbo.FundPrices
ADD CONSTRAINT UQ_FundPrices_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
