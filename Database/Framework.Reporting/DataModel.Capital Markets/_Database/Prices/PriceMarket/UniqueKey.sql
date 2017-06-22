IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].PriceMarket')
	AND		name	= N'UQ_PriceMarket_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_PriceMarket_ApplicationId_Name'
	ALTER	TABLE dbo.PriceMarket
	DROP	CONSTRAINT	UQ_PriceMarket_ApplicationId_Name
END
GO

ALTER TABLE dbo.PriceMarket
ADD CONSTRAINT UQ_PriceMarket_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
