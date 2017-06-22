IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxTradeAndSettleDatesInsert') 
BEGIN
	DROP Procedure TxTradeAndSettleDatesInsert
END
GO

CREATE Procedure dbo.TxTradeAndSettleDatesInsert
(
		@TxTradeAndSettleDatesId				INT		= NULL 	OUTPUT 
	,	@TransactionEventId				INT
	,	@TradeDate				DATETIME
	,	@ContractualDate				DATETIME
	,	@ActualDate				DATETIME
	,	@SpotDate				DATETIME
	,	@SettlementDate				DATETIME
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxTradeAndSettleDates'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TxTradeAndSettleDatesId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId	= @AuditId
	SET @ModifiedByAuditId	= @AuditId

	INSERT INTO dbo.TxTradeAndSettleDates
	(
			TxTradeAndSettleDatesId
		,	TransactionEventId
		,	TradeDate
		,	ContractualDate
		,	ActualDate
		,	SpotDate
		,	SettlementDate
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@TxTradeAndSettleDatesId
		,	@TransactionEventId
		,	@TradeDate
		,	@ContractualDate
		,	@ActualDate
		,	@SpotDate
		,	@SettlementDate
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @TxTradeAndSettleDatesId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TxTradeAndSettleDatesId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
