IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MSPAFileEventInsert') 
BEGIN
	DROP Procedure MSPAFileEventInsert
END
GO

CREATE Procedure dbo.MSPAFileEventInsert
(
		@MSPAFileEventId				INT		= NULL 	OUTPUT 
	,	@Description				VARCHAR(500)
	,	@CreatedBy				VARCHAR(500)
	,	@CreatedOn				DATETIME
	,	@MSPAFileId				INT
	,	@TradingEventTypeId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'MSPAFileEvent'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MSPAFileEventId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.MSPAFileEvent
	(
			MSPAFileEventId
		,	Description
		,	CreatedBy
		,	CreatedOn
		,	MSPAFileId
		,	TradingEventTypeId
		,	ApplicationId
	)
	VALUES
	(
			@MSPAFileEventId
		,	@Description
		,	@CreatedBy
		,	@CreatedOn
		,	@MSPAFileId
		,	@TradingEventTypeId
		,	@ApplicationId
	)

	SELECT @MSPAFileEventId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @MSPAFileEventId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
