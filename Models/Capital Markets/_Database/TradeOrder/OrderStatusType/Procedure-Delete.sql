IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderStatusTypeDelete') 
BEGIN
	DROP Procedure OrderStatusTypeDelete
END
GO

CREATE Procedure dbo.OrderStatusTypeDelete
(
		@OrderStatusTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'OrderStatusType'
)
AS
BEGIN

	DELETE dbo.OrderStatusType
	WHERE	OrderStatusTypeId = @OrderStatusTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @OrderStatusTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
