IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderActionDelete') 
BEGIN
	DROP Procedure OrderActionDelete
END
GO

CREATE Procedure dbo.OrderActionDelete
(
		@OrderActionId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'OrderAction'
)
AS
BEGIN

	DELETE dbo.OrderAction
	WHERE	OrderActionId = @OrderActionId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @OrderActionId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
