IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxTradeInfoInsert') 
BEGIN
	DROP Procedure TxTradeInfoInsert
END
GO

CREATE Procedure dbo.TxTradeInfoInsert
(
		@TxTradeInfoId				INT		= NULL 	OUTPUT 
	,	@TransactionEventId				INT
	,	@TradeCurrencyId				INT
	,	@BuyCurrencyId				INT
	,	@CrossSettlementFXRate				VARCHAR(500)
	,	@NetTradeAmount				VARCHAR(500)
	,	@TradeAccruedInterest				VARCHAR(500)
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxTradeInfo'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TxTradeInfoId Output, @AuditId


	INSERT INTO dbo.TxTradeInfo
	(
			TxTradeInfoId
		,	TransactionEventId
		,	TradeCurrencyId
		,	BuyCurrencyId
		,	CrossSettlementFXRate
		,	NetTradeAmount
		,	TradeAccruedInterest
		,	ApplicationId
	)
	VALUES
	(
			@TxTradeInfoId
		,	@TransactionEventId
		,	@TradeCurrencyId
		,	@BuyCurrencyId
		,	@CrossSettlementFXRate
		,	@NetTradeAmount
		,	@TradeAccruedInterest
		,	@ApplicationId
	)

	SELECT @TxTradeInfoId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TxTradeInfoId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
