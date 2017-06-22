IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MSPAFileEventUpdate') 
BEGIN
	DROP Procedure MSPAFileEventUpdate
END
GO

CREATE Procedure dbo.MSPAFileEventUpdate
(
		@MSPAFileEventId				INT
	,	@Description				VARCHAR(500)
	,	@CreatedBy				VARCHAR(500)
	,	@CreatedOn				DATETIME
	,	@MSPAFileId				INT
	,	@TradingEventTypeId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'MSPAFileEvent'
)
AS
BEGIN

	UPDATE	dbo.MSPAFileEvent
	SET
			Description				=	@Description
		,	CreatedBy				=	@CreatedBy
		,	CreatedOn				=	@CreatedOn
		,	MSPAFileId				=	@MSPAFileId
		,	TradingEventTypeId				=	@TradingEventTypeId
	WHERE	MSPAFileEventId			=   @MSPAFileEventId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MSPAFileEventId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
