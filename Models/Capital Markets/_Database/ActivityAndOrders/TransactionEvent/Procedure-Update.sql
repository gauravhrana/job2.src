IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventUpdate') 
BEGIN
	DROP Procedure TransactionEventUpdate
END
GO

CREATE Procedure dbo.TransactionEventUpdate
(
		@TransactionEventId				INT
	,	@TransactionEventDate				DATETIME
	,	@TransactionSettleDate				DATETIME
	,	@TransactionTypeId				INT
	,	@CustodianId				INT
	,	@StrategyId				INT
	,	@AccountSpecificTypeId				INT
	,	@InvestmentTypeId				INT
	,	@Quantity				INT
	,	@Price				INT
	,	@Fees				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TransactionEvent'
)
AS
BEGIN

	UPDATE	dbo.TransactionEvent
	SET
			TransactionEventDate				=	@TransactionEventDate
		,	TransactionSettleDate				=	@TransactionSettleDate
		,	TransactionTypeId				=	@TransactionTypeId
		,	CustodianId				=	@CustodianId
		,	StrategyId				=	@StrategyId
		,	AccountSpecificTypeId				=	@AccountSpecificTypeId
		,	InvestmentTypeId				=	@InvestmentTypeId
		,	Quantity				=	@Quantity
		,	Price				=	@Price
		,	Fees				=	@Fees
	WHERE	TransactionEventId			=   @TransactionEventId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TransactionEventId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
