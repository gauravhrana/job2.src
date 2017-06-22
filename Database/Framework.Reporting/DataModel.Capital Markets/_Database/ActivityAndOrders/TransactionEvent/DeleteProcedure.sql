IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventDelete') 
	BEGIN
	DROP Procedure TransactionEventDelete
END
GO

CREATE Procedure dbo.TransactionEventDelete
(
		@TransactionEventId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='TransactionEvent'
)
AS
BEGIN
		DELETE dbo.TransactionEvent
		WHERE	TransactionEventId = @TransactionEventId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TransactionEventId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
