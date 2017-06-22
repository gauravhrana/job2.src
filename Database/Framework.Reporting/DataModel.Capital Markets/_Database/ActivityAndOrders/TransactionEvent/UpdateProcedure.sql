IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventUpdate') 
	BEGIN
	DROP Procedure TransactionEventUpdate
END
GO

CREATE Procedure dbo.TransactionEventUpdate
(
		@TransactionEventId			INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TransactionEvent'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT
	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE dbo.TransactionEvent SET
		TransactionEventDate				=	@TransactionEventDate
	,	TransactionSettleDate				=	@TransactionSettleDate
	,	TransactionTypeCode				=	@TransactionTypeCode
	,	CustodianCode				=	@CustodianCode
	,	StrategyCode				=	@StrategyCode
	,	AccountCode				=	@AccountCode
	,	InvestmentCode				=	@InvestmentCode
	,	Quantity				=	@Quantity
	,	Price				=	@Price
	,	Fees				=	@Fees
	,	UpdatedDate				=	@UpdatedDate
	,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE
		TransactionEventId			=   @TransactionEventId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType		= @SystemEntityType
	,	@EntityKey				= @TransactionEventId
	,	@AuditAction			= 'Update'
	,	@CreatedDate			= @AuditDate
	,	@CreatedByPersonId		= @AuditId

END
GO
