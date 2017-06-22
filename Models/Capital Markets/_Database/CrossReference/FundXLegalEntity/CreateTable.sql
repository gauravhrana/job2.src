IF OBJECT_ID ('dbo.FundXLegalEntity') IS NOT NULL
	DROP TABLE dbo.FundXLegalEntity
GO

CREATE TABLE dbo.FundXLegalEntity
(
		FundXLegalEntityId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	FundId			INT		NOT NULL
	,	LegalEntityId			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
