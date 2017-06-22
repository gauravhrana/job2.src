IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].PriceProvider')
	AND		name	= N'UQ_PriceProvider_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_PriceProvider_ApplicationId_Name'
	ALTER	TABLE dbo.PriceProvider
	DROP	CONSTRAINT	UQ_PriceProvider_ApplicationId_Name
END
GO

ALTER TABLE dbo.PriceProvider
ADD CONSTRAINT UQ_PriceProvider_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
