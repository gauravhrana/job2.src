IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxTradeAndSettleDatesUpdate') 
BEGIN
	DROP Procedure TxTradeAndSettleDatesUpdate
END
GO

CREATE Procedure dbo.TxTradeAndSettleDatesUpdate
(
		@TxTradeAndSettleDatesId				INT
	,	@TransactionEventId				INT
	,	@TradeDate				DATETIME
	,	@ContractualDate				DATETIME
	,	@ActualDate				DATETIME
	,	@SpotDate				DATETIME
	,	@SettlementDate				DATETIME
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxTradeAndSettleDates'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.TxTradeAndSettleDates
	SET
			TransactionEventId				=	@TransactionEventId
		,	TradeDate				=	@TradeDate
		,	ContractualDate				=	@ContractualDate
		,	ActualDate				=	@ActualDate
		,	SpotDate				=	@SpotDate
		,	SettlementDate				=	@SettlementDate
		,	UpdatedDate			=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId
	WHERE	TxTradeAndSettleDatesId			=   @TxTradeAndSettleDatesId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TxTradeAndSettleDatesId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
