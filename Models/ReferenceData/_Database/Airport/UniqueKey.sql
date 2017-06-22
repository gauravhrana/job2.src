IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Airport')
	AND		name	= N'UQ_Airport_ApplicationId_CountryId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Airport_ApplicationId_CountryId_Name'
	ALTER	TABLE dbo.Airport
	DROP	CONSTRAINT	UQ_Airport_ApplicationId_CountryId_Name
END
GO

ALTER TABLE dbo.Airport
ADD CONSTRAINT UQ_Airport_ApplicationId_CountryId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, CountryId, Name
)
GO
