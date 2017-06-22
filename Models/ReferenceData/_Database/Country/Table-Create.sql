IF OBJECT_ID ('dbo.Country') IS NOT NULL
	DROP TABLE dbo.Country
GO

CREATE TABLE dbo.Country
(
		CountryId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder				INT		NOT NULL
)
GO
