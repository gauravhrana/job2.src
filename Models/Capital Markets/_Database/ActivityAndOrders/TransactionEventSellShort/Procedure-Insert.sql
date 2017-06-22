IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventSellShortInsert') 
BEGIN
	DROP Procedure TransactionEventSellShortInsert
END
GO

CREATE Procedure dbo.TransactionEventSellShortInsert
(
		@TransactionEventSellShortId				INT		= NULL 	OUTPUT 
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
	,	@SystemEntityType			VARCHAR(50)		= 'TransactionEventSellShort'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TransactionEventSellShortId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.TransactionEventSellShort
	(
			TransactionEventSellShortId
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
			@TransactionEventSellShortId
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

	SELECT @TransactionEventSellShortId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TransactionEventSellShortId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
