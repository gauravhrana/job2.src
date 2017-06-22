IF OBJECT_ID ('dbo.Address') IS NOT NULL
	DROP TABLE dbo.Address
GO

CREATE TABLE dbo.Address
(
		AddressId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	CityId			INT		NOT NULL
	,	StateId			INT		NOT NULL
	,	CountryId			INT		NOT NULL
	,	Address1				VARCHAR(100)		NOT NULL
	,	Address2				VARCHAR(100)		NOT NULL
	,	PostalCode				VARCHAR(100)		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
