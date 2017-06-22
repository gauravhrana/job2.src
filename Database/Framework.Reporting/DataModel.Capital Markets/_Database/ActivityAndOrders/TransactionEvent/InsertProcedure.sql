IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventInsert') 
	BEGIN
	DROP Procedure TransactionEventInsert
END
GO

CREATE Procedure dbo.TransactionEventInsert
(
		@TransactionEventId			INT				= NULL 	OUTPUT 
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		='TransactionEvent'
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

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TransactionEventId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
