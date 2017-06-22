IF OBJECT_ID ('dbo.CommissionType') IS NOT NULL
	DROP TABLE dbo.CommissionType
GO

CREATE TABLE dbo.CommissionType
(
		CommissionTypeId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	CommissionTypeDescription				VARCHAR(100)		NOT NULL
	,	LastModifiedBy				VARCHAR(100)		NOT NULL
	,	LastModifiedOn				DATETIME		NULL
	,	ShowInFilter				INT		NOT NULL
)
GO
