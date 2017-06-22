IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderRequestDelete') 
BEGIN
	DROP Procedure OrderRequestDelete
END
GO

CREATE Procedure dbo.OrderRequestDelete
(
		@OrderRequestId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'OrderRequest'
)
AS
BEGIN

	DELETE dbo.OrderRequest
	WHERE	OrderRequestId = @OrderRequestId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @OrderRequestId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
