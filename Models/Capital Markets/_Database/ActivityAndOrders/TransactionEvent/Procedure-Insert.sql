IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventInsert') 
BEGIN
	DROP Procedure TransactionEventInsert
END
GO

CREATE Procedure dbo.TransactionEventInsert
(
		@TransactionEventId				INT		= NULL 	OUTPUT 
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
	,	@SystemEntityType			VARCHAR(50)		= 'TransactionEvent'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TransactionEventId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.TransactionEvent
	(
			TransactionEventId
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
			@TransactionEventId
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

	SELECT @TransactionEventId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TransactionEventId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
