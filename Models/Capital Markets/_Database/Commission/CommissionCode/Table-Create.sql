IF OBJECT_ID ('dbo.CommissionCode') IS NOT NULL
	DROP TABLE dbo.CommissionCode
GO

CREATE TABLE dbo.CommissionCode
(
		CommissionCodeId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	CommissionCodeCode				VARCHAR(100)		NOT NULL
	,	CommissionCodeDescription				VARCHAR(100)		NOT NULL
	,	BrokerId				INT		NOT NULL
)
GO
