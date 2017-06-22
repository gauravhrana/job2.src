IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventCoverShortUpdate') 
BEGIN
	DROP Procedure TransactionEventCoverShortUpdate
END
GO

CREATE Procedure dbo.TransactionEventCoverShortUpdate
(
		@TransactionEventCoverShortId				INT
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
	,	@SystemEntityType			VARCHAR(50)		= 'TransactionEventCoverShort'
)
AS
BEGIN

	UPDATE	dbo.TransactionEventCoverShort
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
	WHERE	TransactionEventCoverShortId			=   @TransactionEventCoverShortId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TransactionEventCoverShortId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
