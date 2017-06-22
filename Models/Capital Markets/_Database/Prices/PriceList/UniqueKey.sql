IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].PriceList')
	AND		name	= N'UQ_PriceList_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_PriceList_ApplicationId_Name'
	ALTER	TABLE dbo.PriceList
	DROP	CONSTRAINT	UQ_PriceList_ApplicationId_Name
END
GO

ALTER TABLE dbo.PriceList
ADD CONSTRAINT UQ_PriceList_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
