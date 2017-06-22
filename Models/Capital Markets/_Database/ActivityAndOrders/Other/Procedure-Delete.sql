IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxOtherDelete') 
BEGIN
	DROP Procedure TxOtherDelete
END
GO

CREATE Procedure dbo.TxOtherDelete
(
		@TxOtherId				INT		= NULL
	,	@TransactionEventId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TxOther'
)
AS
BEGIN

	DELETE dbo.TxOther
	WHERE		TxOtherId = ISNULL(@TxOtherId, TxOtherId)
	AND			TransactionEventId = ISNULL(@TransactionEventId, TransactionEventId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TxOtherId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
