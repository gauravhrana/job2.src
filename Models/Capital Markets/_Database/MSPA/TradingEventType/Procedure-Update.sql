IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TradingEventTypeUpdate') 
BEGIN
	DROP Procedure TradingEventTypeUpdate
END
GO

CREATE Procedure dbo.TradingEventTypeUpdate
(
		@TradingEventTypeId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TradingEventType'
)
AS
BEGIN

	UPDATE	dbo.TradingEventType
	SET
			Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
	WHERE	TradingEventTypeId			=   @TradingEventTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TradingEventTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
