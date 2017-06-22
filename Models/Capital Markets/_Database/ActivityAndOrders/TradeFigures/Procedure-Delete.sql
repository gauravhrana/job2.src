IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxTradeFiguresDelete') 
BEGIN
	DROP Procedure TxTradeFiguresDelete
END
GO

CREATE Procedure dbo.TxTradeFiguresDelete
(
		@TxTradeFiguresId				INT		= NULL
	,	@TransactionEventId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TxTradeFigures'
)
AS
BEGIN

	DELETE dbo.TxTradeFigures
	WHERE		TxTradeFiguresId = ISNULL(@TxTradeFiguresId, TxTradeFiguresId)
	AND			TransactionEventId = ISNULL(@TransactionEventId, TransactionEventId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TxTradeFiguresId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
