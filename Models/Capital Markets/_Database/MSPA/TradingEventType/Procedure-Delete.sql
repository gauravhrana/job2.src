IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TradingEventTypeDelete') 
BEGIN
	DROP Procedure TradingEventTypeDelete
END
GO

CREATE Procedure dbo.TradingEventTypeDelete
(
		@TradingEventTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TradingEventType'
)
AS
BEGIN

	DELETE dbo.TradingEventType
	WHERE	TradingEventTypeId = @TradingEventTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TradingEventTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
