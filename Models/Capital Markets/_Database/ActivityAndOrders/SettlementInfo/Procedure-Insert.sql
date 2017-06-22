IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxSettlementInfoInsert') 
BEGIN
	DROP Procedure TxSettlementInfoInsert
END
GO

CREATE Procedure dbo.TxSettlementInfoInsert
(
		@TxSettlementInfoId				INT		= NULL 	OUTPUT 
	,	@TransactionEventId				INT
	,	@SettlementCurrencyId				INT
	,	@SellCurrencyId				INT
	,	@TradeDateFXRate				VARCHAR(500)
	,	@NetSettlementAmount				VARCHAR(500)
	,	@NetSettlementCashAmount				VARCHAR(500)
	,	@AccruedInterest				VARCHAR(500)
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxSettlementInfo'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TxSettlementInfoId Output, @AuditId


	INSERT INTO dbo.TxSettlementInfo
	(
			TxSettlementInfoId
		,	TransactionEventId
		,	SettlementCurrencyId
		,	SellCurrencyId
		,	TradeDateFXRate
		,	NetSettlementAmount
		,	NetSettlementCashAmount
		,	AccruedInterest
		,	ApplicationId
	)
	VALUES
	(
			@TxSettlementInfoId
		,	@TransactionEventId
		,	@SettlementCurrencyId
		,	@SellCurrencyId
		,	@TradeDateFXRate
		,	@NetSettlementAmount
		,	@NetSettlementCashAmount
		,	@AccruedInterest
		,	@ApplicationId
	)

	SELECT @TxSettlementInfoId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TxSettlementInfoId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
