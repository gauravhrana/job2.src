IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderStatusDelete') 
BEGIN
	DROP Procedure OrderStatusDelete
END
GO

CREATE Procedure dbo.OrderStatusDelete
(
		@OrderStatusId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'OrderStatus'
)
AS
BEGIN

	DELETE dbo.OrderStatus
	WHERE	OrderStatusId = @OrderStatusId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @OrderStatusId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
