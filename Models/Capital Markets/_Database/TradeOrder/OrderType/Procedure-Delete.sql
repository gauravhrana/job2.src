IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderTypeDelete') 
BEGIN
	DROP Procedure OrderTypeDelete
END
GO

CREATE Procedure dbo.OrderTypeDelete
(
		@OrderTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'OrderType'
)
AS
BEGIN

	DELETE dbo.OrderType
	WHERE	OrderTypeId = @OrderTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @OrderTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
