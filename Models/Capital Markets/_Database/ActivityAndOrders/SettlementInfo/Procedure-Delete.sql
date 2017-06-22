IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxSettlementInfoDelete') 
BEGIN
	DROP Procedure TxSettlementInfoDelete
END
GO

CREATE Procedure dbo.TxSettlementInfoDelete
(
		@TxSettlementInfoId				INT		= NULL
	,	@TransactionEventId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TxSettlementInfo'
)
AS
BEGIN

	DELETE dbo.TxSettlementInfo
	WHERE		TxSettlementInfoId = ISNULL(@TxSettlementInfoId, TxSettlementInfoId)
	AND			TransactionEventId = ISNULL(@TransactionEventId, TransactionEventId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TxSettlementInfoId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
