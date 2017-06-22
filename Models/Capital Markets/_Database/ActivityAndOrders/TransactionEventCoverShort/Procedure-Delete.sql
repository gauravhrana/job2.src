IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventCoverShortDelete') 
BEGIN
	DROP Procedure TransactionEventCoverShortDelete
END
GO

CREATE Procedure dbo.TransactionEventCoverShortDelete
(
		@TransactionEventCoverShortId				INT		= NULL
	,	@TransactionTypeId				INT		= NULL
	,	@CustodianId				INT		= NULL
	,	@StrategyId				INT		= NULL
	,	@AccountSpecificTypeId				INT		= NULL
	,	@InvestmentTypeId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TransactionEventCoverShort'
)
AS
BEGIN

	DELETE dbo.TransactionEventCoverShort
	WHERE		TransactionEventCoverShortId = ISNULL(@TransactionEventCoverShortId, TransactionEventCoverShortId)
	AND			TransactionTypeId = ISNULL(@TransactionTypeId, TransactionTypeId)
	AND			CustodianId = ISNULL(@CustodianId, CustodianId)
	AND			StrategyId = ISNULL(@StrategyId, StrategyId)
	AND			AccountSpecificTypeId = ISNULL(@AccountSpecificTypeId, AccountSpecificTypeId)
	AND			InvestmentTypeId = ISNULL(@InvestmentTypeId, InvestmentTypeId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TransactionEventCoverShortId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
