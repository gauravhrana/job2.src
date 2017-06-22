IF OBJECT_ID ('dbo.HolidayXCountry') IS NOT NULL
	DROP TABLE dbo.HolidayXCountry
GO

CREATE TABLE dbo.HolidayXCountry
(
		HolidayXCountryId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	HolidayId				INT		NOT NULL
	,	CountryId				INT		NOT NULL
)
GO
