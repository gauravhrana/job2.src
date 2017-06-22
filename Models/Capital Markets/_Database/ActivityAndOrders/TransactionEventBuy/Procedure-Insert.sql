IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventBuyInsert') 
BEGIN
	DROP Procedure TransactionEventBuyInsert
END
GO

CREATE Procedure dbo.TransactionEventBuyInsert
(
		@TransactionEventBuyId				INT		= NULL 	OUTPUT 
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
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TransactionEventBuy'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TransactionEventBuyId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.TransactionEventBuy
	(
			TransactionEventBuyId
		,	TransactionEventDate
		,	TransactionSettleDate
		,	TransactionTypeId
		,	CustodianId
		,	StrategyId
		,	AccountSpecificTypeId
		,	InvestmentTypeId
		,	Quantity
		,	Price
		,	Fees
		,	ApplicationId
	)
	VALUES
	(
			@TransactionEventBuyId
		,	@TransactionEventDate
		,	@TransactionSettleDate
		,	@TransactionTypeId
		,	@CustodianId
		,	@StrategyId
		,	@AccountSpecificTypeId
		,	@InvestmentTypeId
		,	@Quantity
		,	@Price
		,	@Fees
		,	@ApplicationId
	)

	SELECT @TransactionEventBuyId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TransactionEventBuyId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
