IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventDelete') 
BEGIN
	DROP Procedure TransactionEventDelete
END
GO

CREATE Procedure dbo.TransactionEventDelete
(
		@TransactionEventId				INT		= NULL
	,	@TransactionTypeId				INT		= NULL
	,	@CustodianId				INT		= NULL
	,	@StrategyId				INT		= NULL
	,	@AccountSpecificTypeId				INT		= NULL
	,	@InvestmentTypeId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TransactionEvent'
)
AS
BEGIN

	DELETE dbo.TransactionEvent
	WHERE		TransactionEventId = ISNULL(@TransactionEventId, TransactionEventId)
	AND			TransactionTypeId = ISNULL(@TransactionTypeId, TransactionTypeId)
	AND			CustodianId = ISNULL(@CustodianId, CustodianId)
	AND			StrategyId = ISNULL(@StrategyId, StrategyId)
	AND			AccountSpecificTypeId = ISNULL(@AccountSpecificTypeId, AccountSpecificTypeId)
	AND			InvestmentTypeId = ISNULL(@InvestmentTypeId, InvestmentTypeId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TransactionEventId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
