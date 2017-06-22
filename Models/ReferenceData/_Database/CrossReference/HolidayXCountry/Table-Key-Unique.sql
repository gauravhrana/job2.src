IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].HolidayXCountry')
	AND		name	= N'UQ_HolidayXCountry_ApplicationId_HolidayId_CountryId'
)
BEGIN
	PRINT	'Dropping UQ_HolidayXCountry_ApplicationId_HolidayId_CountryId'
	ALTER	TABLE dbo.HolidayXCountry
	DROP	CONSTRAINT	UQ_HolidayXCountry_ApplicationId_HolidayId_CountryId
END
GO

ALTER TABLE dbo.HolidayXCountry
ADD CONSTRAINT UQ_HolidayXCountry_ApplicationId_HolidayId_CountryId UNIQUE NONCLUSTERED
(
	ApplicationId, HolidayId, CountryId
)
GO
