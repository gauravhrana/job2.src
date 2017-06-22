IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventSellShortUpdate') 
BEGIN
	DROP Procedure TransactionEventSellShortUpdate
END
GO

CREATE Procedure dbo.TransactionEventSellShortUpdate
(
		@TransactionEventSellShortId				INT
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
	,	@SystemEntityType			VARCHAR(50)		= 'TransactionEventSellShort'
)
AS
BEGIN

	UPDATE	dbo.TransactionEventSellShort
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
	WHERE	TransactionEventSellShortId			=   @TransactionEventSellShortId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TransactionEventSellShortId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
