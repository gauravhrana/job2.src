IF OBJECT_ID ('dbo.CreditFacility') IS NOT NULL
	DROP TABLE dbo.CreditFacility
GO

CREATE TABLE dbo.CreditFacility
(
		CreditFacilityId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
