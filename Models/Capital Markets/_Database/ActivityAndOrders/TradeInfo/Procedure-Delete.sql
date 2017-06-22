IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxTradeInfoDelete') 
BEGIN
	DROP Procedure TxTradeInfoDelete
END
GO

CREATE Procedure dbo.TxTradeInfoDelete
(
		@TxTradeInfoId				INT		= NULL
	,	@TransactionEventId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TxTradeInfo'
)
AS
BEGIN

	DELETE dbo.TxTradeInfo
	WHERE		TxTradeInfoId = ISNULL(@TxTradeInfoId, TxTradeInfoId)
	AND			TransactionEventId = ISNULL(@TransactionEventId, TransactionEventId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TxTradeInfoId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
