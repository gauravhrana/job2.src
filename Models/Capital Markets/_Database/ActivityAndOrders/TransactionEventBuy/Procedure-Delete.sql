IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventBuyDelete') 
BEGIN
	DROP Procedure TransactionEventBuyDelete
END
GO

CREATE Procedure dbo.TransactionEventBuyDelete
(
		@TransactionEventBuyId				INT		= NULL
	,	@TransactionTypeId				INT		= NULL
	,	@CustodianId				INT		= NULL
	,	@StrategyId				INT		= NULL
	,	@AccountSpecificTypeId				INT		= NULL
	,	@InvestmentTypeId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TransactionEventBuy'
)
AS
BEGIN

	DELETE dbo.TransactionEventBuy
	WHERE		TransactionEventBuyId = ISNULL(@TransactionEventBuyId, TransactionEventBuyId)
	AND			TransactionTypeId = ISNULL(@TransactionTypeId, TransactionTypeId)
	AND			CustodianId = ISNULL(@CustodianId, CustodianId)
	AND			StrategyId = ISNULL(@StrategyId, StrategyId)
	AND			AccountSpecificTypeId = ISNULL(@AccountSpecificTypeId, AccountSpecificTypeId)
	AND			InvestmentTypeId = ISNULL(@InvestmentTypeId, InvestmentTypeId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TransactionEventBuyId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
