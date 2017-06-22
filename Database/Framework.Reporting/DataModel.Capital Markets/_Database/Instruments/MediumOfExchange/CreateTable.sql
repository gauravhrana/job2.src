IF OBJECT_ID ('dbo.MediumOfExchange') IS NOT NULL
	DROP TABLE dbo.MediumOfExchange
GO

CREATE TABLE dbo.MediumOfExchange
(
		MediumOfExchangeId			INT		NOT NULL
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
