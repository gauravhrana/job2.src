IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].MarketCapitalizationCategory')
	AND		name	= N'UQ_MarketCapitalizationCategory_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_MarketCapitalizationCategory_ApplicationId_Name'
	ALTER	TABLE dbo.MarketCapitalizationCategory
	DROP	CONSTRAINT	UQ_MarketCapitalizationCategory_ApplicationId_Name
END
GO

ALTER TABLE dbo.MarketCapitalizationCategory
ADD CONSTRAINT UQ_MarketCapitalizationCategory_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
