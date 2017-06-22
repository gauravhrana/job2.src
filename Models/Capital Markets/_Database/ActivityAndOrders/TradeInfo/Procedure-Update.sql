IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxTradeInfoUpdate') 
BEGIN
	DROP Procedure TxTradeInfoUpdate
END
GO

CREATE Procedure dbo.TxTradeInfoUpdate
(
		@TxTradeInfoId				INT
	,	@TransactionEventId				INT
	,	@TradeCurrencyId				INT
	,	@BuyCurrencyId				INT
	,	@CrossSettlementFXRate				VARCHAR(500)
	,	@NetTradeAmount				VARCHAR(500)
	,	@TradeAccruedInterest				VARCHAR(500)
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxTradeInfo'
)
AS
BEGIN
			TransactionEventId				=	@TransactionEventId
		,	TradeCurrencyId				=	@TradeCurrencyId
		,	BuyCurrencyId				=	@BuyCurrencyId
		,	CrossSettlementFXRate				=	@CrossSettlementFXRate
		,	NetTradeAmount				=	@NetTradeAmount
		,	TradeAccruedInterest				=	@TradeAccruedInterest
	WHERE	TxTradeInfoId			=   @TxTradeInfoId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TxTradeInfoId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
