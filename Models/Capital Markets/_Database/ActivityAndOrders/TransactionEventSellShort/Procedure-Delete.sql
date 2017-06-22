IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventSellShortDelete') 
BEGIN
	DROP Procedure TransactionEventSellShortDelete
END
GO

CREATE Procedure dbo.TransactionEventSellShortDelete
(
		@TransactionEventSellShortId				INT		= NULL
	,	@TransactionTypeId				INT		= NULL
	,	@CustodianId				INT		= NULL
	,	@StrategyId				INT		= NULL
	,	@AccountSpecificTypeId				INT		= NULL
	,	@InvestmentTypeId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TransactionEventSellShort'
)
AS
BEGIN

	DELETE dbo.TransactionEventSellShort
	WHERE		TransactionEventSellShortId = ISNULL(@TransactionEventSellShortId, TransactionEventSellShortId)
	AND			TransactionTypeId = ISNULL(@TransactionTypeId, TransactionTypeId)
	AND			CustodianId = ISNULL(@CustodianId, CustodianId)
	AND			StrategyId = ISNULL(@StrategyId, StrategyId)
	AND			AccountSpecificTypeId = ISNULL(@AccountSpecificTypeId, AccountSpecificTypeId)
	AND			InvestmentTypeId = ISNULL(@InvestmentTypeId, InvestmentTypeId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TransactionEventSellShortId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
