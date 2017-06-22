IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventBuyUpdate') 
BEGIN
	DROP Procedure TransactionEventBuyUpdate
END
GO

CREATE Procedure dbo.TransactionEventBuyUpdate
(
		@TransactionEventBuyId				INT
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
	,	@SystemEntityType			VARCHAR(50)		= 'TransactionEventBuy'
)
AS
BEGIN

	UPDATE	dbo.TransactionEventBuy
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
	WHERE	TransactionEventBuyId			=   @TransactionEventBuyId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TransactionEventBuyId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
