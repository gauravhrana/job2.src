IF OBJECT_ID ('dbo.CommissionRate') IS NOT NULL
	DROP TABLE dbo.CommissionRate
GO

CREATE TABLE dbo.CommissionRate
(
		CommissionRateId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	ClearingRate				DECIMAL(18, 5)		NOT NULL
	,	ExecutionRate				DECIMAL(18, 5)		NOT NULL
	,	BrokerId				INT		NOT NULL
	,	ExchangeId				INT		NOT NULL
)
GO
