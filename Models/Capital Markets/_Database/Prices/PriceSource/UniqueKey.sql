IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].PriceSource')
	AND		name	= N'UQ_PriceSource_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_PriceSource_ApplicationId_Name'
	ALTER	TABLE dbo.PriceSource
	DROP	CONSTRAINT	UQ_PriceSource_ApplicationId_Name
END
GO

ALTER TABLE dbo.PriceSource
ADD CONSTRAINT UQ_PriceSource_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
