IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderItemDelete') 
BEGIN
	DROP Procedure OrderItemDelete
END
GO

CREATE Procedure dbo.OrderItemDelete
(
		@OrderItemId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'OrderItem'
)
AS
BEGIN

	DELETE dbo.OrderItem
	WHERE	OrderItemId = @OrderItemId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @OrderItemId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
