IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='NeedItemDelete') 
	BEGIN
	DROP Procedure NeedItemDelete
END
GO

CREATE Procedure dbo.NeedItemDelete
(
		@NeedItemId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='NeedItem'
)
AS
BEGIN
		DELETE dbo.NeedItem
		WHERE	NeedItemId = @NeedItemId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @NeedItemId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
