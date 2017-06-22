IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderActionInsert') 
BEGIN
	DROP Procedure OrderActionInsert
END
GO

CREATE Procedure dbo.OrderActionInsert
(
		@OrderActionId				INT		= NULL 	OUTPUT 
	,	@OrderActionCode				VARCHAR(500)
	,	@OrderActionDescription				VARCHAR(500)
	,	@PositionDirection				VARCHAR(500)
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderAction'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @OrderActionId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.OrderAction
	(
			OrderActionId
		,	OrderActionCode
		,	OrderActionDescription
		,	PositionDirection
		,	ApplicationId
	)
	VALUES
	(
			@OrderActionId
		,	@OrderActionCode
		,	@OrderActionDescription
		,	@PositionDirection
		,	@ApplicationId
	)

	SELECT @OrderActionId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @OrderActionId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
