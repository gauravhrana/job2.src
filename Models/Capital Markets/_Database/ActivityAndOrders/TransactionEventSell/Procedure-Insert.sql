IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventSellInsert') 
BEGIN
	DROP Procedure TransactionEventSellInsert
END
GO

CREATE Procedure dbo.TransactionEventSellInsert
(
		@TransactionEventSellId				INT		= NULL 	OUTPUT 
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
	,	@SystemEntityType			VARCHAR(50)		= 'TransactionEventSell'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TransactionEventSellId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.TransactionEventSell
	(
			TransactionEventSellId
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
			@TransactionEventSellId
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

	SELECT @TransactionEventSellId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TransactionEventSellId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
