IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionTypeDelete') 
BEGIN
	DROP Procedure TransactionTypeDelete
END
GO

CREATE Procedure dbo.TransactionTypeDelete
(
		@TransactionTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TransactionType'
)
AS
BEGIN

	DELETE dbo.TransactionType
	WHERE	TransactionTypeId = @TransactionTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TransactionTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
