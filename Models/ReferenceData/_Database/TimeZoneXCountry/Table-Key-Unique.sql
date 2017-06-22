IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TimeZoneXCountry')
	AND		name	= N'UQ_TimeZoneXCountry_ApplicationId_TimeZoneId_CountryId'
)
BEGIN
	PRINT	'Dropping UQ_TimeZoneXCountry_ApplicationId_TimeZoneId_CountryId'
	ALTER	TABLE dbo.TimeZoneXCountry
	DROP	CONSTRAINT	UQ_TimeZoneXCountry_ApplicationId_TimeZoneId_CountryId
END
GO

ALTER TABLE dbo.TimeZoneXCountry
ADD CONSTRAINT UQ_TimeZoneXCountry_ApplicationId_TimeZoneId_CountryId UNIQUE NONCLUSTERED
(
	ApplicationId, TimeZoneId, CountryId
)
GO
