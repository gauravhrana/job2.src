IF OBJECT_ID ('dbo.Position') IS NOT NULL
	DROP TABLE dbo.Position
GO

CREATE TABLE dbo.Position
(
		PositionId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	InvestmentCode				VARCHAR(100)		NOT NULL
	,	PeriodDate				DATETIME		NULL
	,	CustodianCode				VARCHAR(100)		NOT NULL
	,	StrategyCode				VARCHAR(100)		NOT NULL
	,	AccountCode				VARCHAR(100)		NOT NULL
	,	Quantity				DECIMAL(18, 5)		NOT NULL
	,	CostBasis				DECIMAL(18, 5)		NOT NULL
	,	MarketValue				DECIMAL(18, 5)		NOT NULL
	,	StartMarketValue				DECIMAL(18, 5)		NOT NULL
	,	DeltaAdjustedExposure				DECIMAL(18, 5)		NOT NULL
	,	StartDeltaAdjustedExposure				DECIMAL(18, 5)		NOT NULL
	,	RealizedPnL				DECIMAL(18, 5)		NOT NULL
	,	UnrealizedPnL				DECIMAL(18, 5)		NOT NULL
	,	Mark				INT		NOT NULL
)
GO
