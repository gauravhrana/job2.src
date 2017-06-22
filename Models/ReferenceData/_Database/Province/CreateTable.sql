IF OBJECT_ID ('dbo.Province') IS NOT NULL
	DROP TABLE dbo.Province
GO

CREATE TABLE dbo.Province
(
		ProvinceId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	CountryId			INT		NOT NULL
	,	ProvinceTypeId			INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
