IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventSellDelete') 
BEGIN
	DROP Procedure TransactionEventSellDelete
END
GO

CREATE Procedure dbo.TransactionEventSellDelete
(
		@TransactionEventSellId				INT		= NULL
	,	@TransactionTypeId				INT		= NULL
	,	@CustodianId				INT		= NULL
	,	@StrategyId				INT		= NULL
	,	@AccountSpecificTypeId				INT		= NULL
	,	@InvestmentTypeId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TransactionEventSell'
)
AS
BEGIN

	DELETE dbo.TransactionEventSell
	WHERE		TransactionEventSellId = ISNULL(@TransactionEventSellId, TransactionEventSellId)
	AND			TransactionTypeId = ISNULL(@TransactionTypeId, TransactionTypeId)
	AND			CustodianId = ISNULL(@CustodianId, CustodianId)
	AND			StrategyId = ISNULL(@StrategyId, StrategyId)
	AND			AccountSpecificTypeId = ISNULL(@AccountSpecificTypeId, AccountSpecificTypeId)
	AND			InvestmentTypeId = ISNULL(@InvestmentTypeId, InvestmentTypeId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TransactionEventSellId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
