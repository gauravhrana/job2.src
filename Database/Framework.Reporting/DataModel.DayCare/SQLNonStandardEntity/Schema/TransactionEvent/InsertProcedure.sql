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
	,	@TransactionTypeCode				VARCHAR(500)
	,	@CustodianCode				VARCHAR(500)
	,	@StrategyCode				VARCHAR(500)
	,	@AccountCode				VARCHAR(500)
	,	@InvestmentCode				VARCHAR(500)
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

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@TransactionEventId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.TransactionEvent
	(
			TransactionEventId
		,	TransactionEventDate
		,	TransactionSettleDate
		,	TransactionTypeCode
		,	CustodianCode
		,	StrategyCode
		,	AccountCode
		,	InvestmentCode
		,	Quantity
		,	Price
		,	Fees
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@TransactionEventId
		,	@TransactionEventDate
		,	@TransactionSettleDate
		,	@TransactionTypeCode
		,	@CustodianCode
		,	@StrategyCode
		,	@AccountCode
		,	@InvestmentCode
		,	@Quantity
		,	@Price
		,	@Fees
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
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
