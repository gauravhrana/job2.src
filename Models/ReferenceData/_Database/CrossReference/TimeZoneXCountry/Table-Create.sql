IF OBJECT_ID ('dbo.TimeZoneXCountry') IS NOT NULL
	DROP TABLE dbo.TimeZoneXCountry
GO

CREATE TABLE dbo.TimeZoneXCountry
(
		TimeZoneXCountryId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	TimeZoneId				INT		NOT NULL
	,	CountryId				INT		NOT NULL
)
GO
