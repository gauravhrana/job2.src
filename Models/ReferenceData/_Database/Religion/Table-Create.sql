IF OBJECT_ID ('dbo.Religion') IS NOT NULL
	DROP TABLE dbo.Religion
GO

CREATE TABLE dbo.Religion
(
		ReligionId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder				INT		NOT NULL
)
GO
