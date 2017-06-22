IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxTradeFiguresUpdate') 
BEGIN
	DROP Procedure TxTradeFiguresUpdate
END
GO

CREATE Procedure dbo.TxTradeFiguresUpdate
(
		@TxTradeFiguresId				INT
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
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxTradeFigures'
)
AS
BEGIN
			TransactionEventId				=	@TransactionEventId
		,	Quantity				=	@Quantity
		,	Price				=	@Price
		,	TotalCommission				=	@TotalCommission
		,	BrokerCodeId				=	@BrokerCodeId
		,	GlobalFacilityAmount				=	@GlobalFacilityAmount
		,	ExemptUnrealizedPLfromCapitalRatios				=	@ExemptUnrealizedPLfromCapitalRatios
		,	InternalTradeExcludeInByPassStrategy				=	@InternalTradeExcludeInByPassStrategy
		,	ForwardFXBookCurrencyPricing				=	@ForwardFXBookCurrencyPricing
		,	OriginalFace				=	@OriginalFace
		,	IndexRatio				=	@IndexRatio
		,	PerShareAmount				=	@PerShareAmount
		,	OpeningRate				=	@OpeningRate
		,	PercentageOwned				=	@PercentageOwned
		,	DelayedCompensationId				=	@DelayedCompensationId
		,	ReceiveFinancing				=	@ReceiveFinancing
		,	Yield				=	@Yield
		,	NotionalAmount				=	@NotionalAmount
		,	TradesAsId				=	@TradesAsId
		,	DirtyPrice				=	@DirtyPrice
		,	TradesFlat				=	@TradesFlat
		,	RestateUnrealizedGainOrLossAtPeriodEndSpotRate				=	@RestateUnrealizedGainOrLossAtPeriodEndSpotRate
		,	OverridingFinancingId				=	@OverridingFinancingId
		,	AccrueCommission				=	@AccrueCommission
		,	EffectiveYield				=	@EffectiveYield
		,	NetTrade				=	@NetTrade
		,	PayOrReceiveFullCoupon				=	@PayOrReceiveFullCoupon
		,	ExpirationDate				=	@ExpirationDate
		,	SweepCashOnSettlementDate				=	@SweepCashOnSettlementDate
	WHERE	TxTradeFiguresId			=   @TxTradeFiguresId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TxTradeFiguresId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
