IF OBJECT_ID ('dbo.State') IS NOT NULL
	DROP TABLE dbo.State
GO

CREATE TABLE dbo.State
(
		StateId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	CountryId				INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder				INT		NOT NULL
)
GO
