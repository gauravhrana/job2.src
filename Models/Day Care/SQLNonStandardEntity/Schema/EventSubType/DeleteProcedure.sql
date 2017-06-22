IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='EventSubTypeDelete') 
BEGIN
	DROP Procedure EventSubTypeDelete
END
GO

CREATE Procedure dbo.EventSubTypeDelete
(
		@EventSubTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'EventSubType'
)
AS
BEGIN

	DELETE dbo.EventSubType
	WHERE	EventSubTypeId = @EventSubTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @EventSubTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
