IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxSettlementInfoUpdate') 
BEGIN
	DROP Procedure TxSettlementInfoUpdate
END
GO

CREATE Procedure dbo.TxSettlementInfoUpdate
(
		@TxSettlementInfoId				INT
	,	@TransactionEventId				INT
	,	@SettlementCurrencyId				INT
	,	@SellCurrencyId				INT
	,	@TradeDateFXRate				VARCHAR(500)
	,	@NetSettlementAmount				VARCHAR(500)
	,	@NetSettlementCashAmount				VARCHAR(500)
	,	@AccruedInterest				VARCHAR(500)
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxSettlementInfo'
)
AS
BEGIN
			TransactionEventId				=	@TransactionEventId
		,	SettlementCurrencyId				=	@SettlementCurrencyId
		,	SellCurrencyId				=	@SellCurrencyId
		,	TradeDateFXRate				=	@TradeDateFXRate
		,	NetSettlementAmount				=	@NetSettlementAmount
		,	NetSettlementCashAmount				=	@NetSettlementCashAmount
		,	AccruedInterest				=	@AccruedInterest
	WHERE	TxSettlementInfoId			=   @TxSettlementInfoId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TxSettlementInfoId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
