IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxTradeFiguresInsert') 
BEGIN
	DROP Procedure TxTradeFiguresInsert
END
GO

CREATE Procedure dbo.TxTradeFiguresInsert
(
		@TxTradeFiguresId				INT		= NULL 	OUTPUT 
	,	@TransactionEventId				INT
	,	@Quantity				INT
	,	@Price				INT
	,	@TotalCommission				VARCHAR(500)
	,	@BrokerCodeId				INT
	,	@GlobalFacilityAmount				VARCHAR(500)
	,	@ExemptUnrealizedPLfromCapitalRatios				VARCHAR(500)
	,	@InternalTradeExcludeInByPassStrategy				VARCHAR(500)
	,	@ForwardFXBookCurrencyPricing				VARCHAR(500)
	,	@OriginalFace				VARCHAR(500)
	,	@IndexRatio				VARCHAR(500)
	,	@PerShareAmount				VARCHAR(500)
	,	@OpeningRate				VARCHAR(500)
	,	@PercentageOwned				VARCHAR(500)
	,	@DelayedCompensationId				INT
	,	@ReceiveFinancing				VARCHAR(500)
	,	@Yield				VARCHAR(500)
	,	@NotionalAmount				VARCHAR(500)
	,	@TradesAsId				INT
	,	@DirtyPrice				VARCHAR(500)
	,	@TradesFlat				VARCHAR(500)
	,	@RestateUnrealizedGainOrLossAtPeriodEndSpotRate				VARCHAR(500)
	,	@OverridingFinancingId				INT
	,	@AccrueCommission				VARCHAR(500)
	,	@EffectiveYield				VARCHAR(500)
	,	@NetTrade				VARCHAR(500)
	,	@PayOrReceiveFullCoupon				VARCHAR(500)
	,	@ExpirationDate				VARCHAR(500)
	,	@SweepCashOnSettlementDate				VARCHAR(500)
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxTradeFigures'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TxTradeFiguresId Output, @AuditId


	INSERT INTO dbo.TxTradeFigures
	(
			TxTradeFiguresId
		,	TransactionEventId
		,	Quantity
		,	Price
		,	TotalCommission
		,	BrokerCodeId
		,	GlobalFacilityAmount
		,	ExemptUnrealizedPLfromCapitalRatios
		,	InternalTradeExcludeInByPassStrategy
		,	ForwardFXBookCurrencyPricing
		,	OriginalFace
		,	IndexRatio
		,	PerShareAmount
		,	OpeningRate
		,	PercentageOwned
		,	DelayedCompensationId
		,	ReceiveFinancing
		,	Yield
		,	NotionalAmount
		,	TradesAsId
		,	DirtyPrice
		,	TradesFlat
		,	RestateUnrealizedGainOrLossAtPeriodEndSpotRate
		,	OverridingFinancingId
		,	AccrueCommission
		,	EffectiveYield
		,	NetTrade
		,	PayOrReceiveFullCoupon
		,	ExpirationDate
		,	SweepCashOnSettlementDate
		,	ApplicationId
	)
	VALUES
	(
			@TxTradeFiguresId
		,	@TransactionEventId
		,	@Quantity
		,	@Price
		,	@TotalCommission
		,	@BrokerCodeId
		,	@GlobalFacilityAmount
		,	@ExemptUnrealizedPLfromCapitalRatios
		,	@InternalTradeExcludeInByPassStrategy
		,	@ForwardFXBookCurrencyPricing
		,	@OriginalFace
		,	@IndexRatio
		,	@PerShareAmount
		,	@OpeningRate
		,	@PercentageOwned
		,	@DelayedCompensationId
		,	@ReceiveFinancing
		,	@Yield
		,	@NotionalAmount
		,	@TradesAsId
		,	@DirtyPrice
		,	@TradesFlat
		,	@RestateUnrealizedGainOrLossAtPeriodEndSpotRate
		,	@OverridingFinancingId
		,	@AccrueCommission
		,	@EffectiveYield
		,	@NetTrade
		,	@PayOrReceiveFullCoupon
		,	@ExpirationDate
		,	@SweepCashOnSettlementDate
		,	@ApplicationId
	)

	SELECT @TxTradeFiguresId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TxTradeFiguresId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
