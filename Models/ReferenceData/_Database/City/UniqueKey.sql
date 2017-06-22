IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].City')
	AND		name	= N'UQ_City_ApplicationId_CountryId_Name'
)
BEGIN
	PRINT	'Dropping UQ_City_ApplicationId_CountryId_Name'
	ALTER	TABLE dbo.City
	DROP	CONSTRAINT	UQ_City_ApplicationId_CountryId_Name
END
GO

ALTER TABLE dbo.City
ADD CONSTRAINT UQ_City_ApplicationId_CountryId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, CountryId, Name
)
GO
