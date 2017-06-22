IF OBJECT_ID ('dbo.TxTradeFigures') IS NOT NULL
	DROP TABLE dbo.TxTradeFigures
GO

CREATE TABLE dbo.TxTradeFigures
(
		TxTradeFiguresId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	TransactionEventId				INT		NOT NULL
	,	Quantity				INT		NOT NULL
	,	Price				INT		NOT NULL
	,	TotalCommission				VARCHAR(100)		NOT NULL
	,	BrokerCodeId				INT		NOT NULL
	,	GlobalFacilityAmount				VARCHAR(100)		NOT NULL
	,	ExemptUnrealizedPLfromCapitalRatios				VARCHAR(100)		NOT NULL
	,	InternalTradeExcludeInByPassStrategy				VARCHAR(100)		NOT NULL
	,	ForwardFXBookCurrencyPricing				VARCHAR(100)		NOT NULL
	,	OriginalFace				VARCHAR(100)		NOT NULL
	,	IndexRatio				VARCHAR(100)		NOT NULL
	,	PerShareAmount				VARCHAR(100)		NOT NULL
	,	OpeningRate				VARCHAR(100)		NOT NULL
	,	PercentageOwned				VARCHAR(100)		NOT NULL
	,	DelayedCompensationId				INT		NOT NULL
	,	ReceiveFinancing				VARCHAR(100)		NOT NULL
	,	Yield				VARCHAR(100)		NOT NULL
	,	NotionalAmount				VARCHAR(100)		NOT NULL
	,	TradesAsId				INT		NOT NULL
	,	DirtyPrice				VARCHAR(100)		NOT NULL
	,	TradesFlat				VARCHAR(100)		NOT NULL
	,	RestateUnrealizedGainOrLossAtPeriodEndSpotRate				VARCHAR(100)		NOT NULL
	,	OverridingFinancingId				INT		NOT NULL
	,	AccrueCommission				VARCHAR(100)		NOT NULL
	,	EffectiveYield				VARCHAR(100)		NOT NULL
	,	NetTrade				VARCHAR(100)		NOT NULL
	,	PayOrReceiveFullCoupon				VARCHAR(100)		NOT NULL
	,	ExpirationDate				VARCHAR(100)		NOT NULL
	,	SweepCashOnSettlementDate				VARCHAR(100)		NOT NULL
)
GO
