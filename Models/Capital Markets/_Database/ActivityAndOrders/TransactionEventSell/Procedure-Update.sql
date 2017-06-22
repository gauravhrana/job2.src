IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventSellUpdate') 
BEGIN
	DROP Procedure TransactionEventSellUpdate
END
GO

CREATE Procedure dbo.TransactionEventSellUpdate
(
		@TransactionEventSellId				INT
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
	,	@SystemEntityType			VARCHAR(50)		= 'TransactionEventSell'
)
AS
BEGIN

	UPDATE	dbo.TransactionEventSell
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
	WHERE	TransactionEventSellId			=   @TransactionEventSellId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TransactionEventSellId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
