IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventCoverShortInsert') 
BEGIN
	DROP Procedure TransactionEventCoverShortInsert
END
GO

CREATE Procedure dbo.TransactionEventCoverShortInsert
(
		@TransactionEventCoverShortId				INT		= NULL 	OUTPUT 
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
	,	@SystemEntityType			VARCHAR(50)		= 'TransactionEventCoverShort'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TransactionEventCoverShortId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.TransactionEventCoverShort
	(
			TransactionEventCoverShortId
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
			@TransactionEventCoverShortId
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

	SELECT @TransactionEventCoverShortId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TransactionEventCoverShortId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
