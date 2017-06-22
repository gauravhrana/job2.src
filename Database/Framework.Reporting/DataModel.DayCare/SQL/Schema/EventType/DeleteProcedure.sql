IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='EventTypeDelete') 
	BEGIN
	DROP Procedure EventTypeDelete
END
GO

CREATE Procedure dbo.EventTypeDelete
(
		@EventTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='EventType'
)
AS
BEGIN
		DELETE dbo.EventType
		WHERE	EventTypeId = @EventTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @EventTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
