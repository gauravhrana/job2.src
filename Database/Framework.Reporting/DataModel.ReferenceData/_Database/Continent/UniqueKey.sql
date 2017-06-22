IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Continent')
	AND		name	= N'UQ_Continent_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Continent_ApplicationId_Name'
	ALTER	TABLE dbo.Continent
	DROP	CONSTRAINT	UQ_Continent_ApplicationId_Name
END
GO

ALTER TABLE dbo.Continent
ADD CONSTRAINT UQ_Continent_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
