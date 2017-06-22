IF OBJECT_ID ('dbo.CaseType') IS NOT NULL
	DROP TABLE dbo.CaseType
GO

CREATE TABLE dbo.CaseType
(
		CaseTypeId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder				INT		NOT NULL
)
GO
