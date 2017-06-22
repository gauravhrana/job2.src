IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxTradeAndSettleDatesDelete') 
BEGIN
	DROP Procedure TxTradeAndSettleDatesDelete
END
GO

CREATE Procedure dbo.TxTradeAndSettleDatesDelete
(
		@TxTradeAndSettleDatesId				INT		= NULL
	,	@TransactionEventId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TxTradeAndSettleDates'
)
AS
BEGIN

	DELETE dbo.TxTradeAndSettleDates
	WHERE		TxTradeAndSettleDatesId = ISNULL(@TxTradeAndSettleDatesId, TxTradeAndSettleDatesId)
	AND			TransactionEventId = ISNULL(@TransactionEventId, TransactionEventId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TxTradeAndSettleDatesId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
