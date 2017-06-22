IF OBJECT_ID ('dbo.City') IS NOT NULL
	DROP TABLE dbo.City
GO

CREATE TABLE dbo.City
(
		CityId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	CountryId				INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder				INT		NOT NULL
)
GO
