IF OBJECT_ID ('dbo.CountryXReligion') IS NOT NULL
	DROP TABLE dbo.CountryXReligion
GO

CREATE TABLE dbo.CountryXReligion
(
		CountryXReligionId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	CountryId				INT		NOT NULL
	,	ReligionId				INT		NOT NULL
)
GO
