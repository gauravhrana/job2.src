IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderStatusGroupDelete') 
BEGIN
	DROP Procedure OrderStatusGroupDelete
END
GO

CREATE Procedure dbo.OrderStatusGroupDelete
(
		@OrderStatusGroupId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'OrderStatusGroup'
)
AS
BEGIN

	DELETE dbo.OrderStatusGroup
	WHERE	OrderStatusGroupId = @OrderStatusGroupId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @OrderStatusGroupId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
