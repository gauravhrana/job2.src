IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='EventSubTypeInsert') 
BEGIN
	DROP Procedure EventSubTypeInsert
END
GO

CREATE Procedure dbo.EventSubTypeInsert
(
		@EventSubTypeId				INT		= NULL 	OUTPUT 
	,	@EventTypeId				INT
	,	@PersonId				INT
	,	@EventKey				VARCHAR(500)
	,	@SortOrder				INT
	,	@Value				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'EventSubType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@EventSubTypeId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.EventSubType
	(
			EventSubTypeId
		,	EventTypeId
		,	PersonId
		,	EventKey
		,	SortOrder
		,	Value
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@EventSubTypeId
		,	@EventTypeId
		,	@PersonId
		,	@EventKey
		,	@SortOrder
		,	@Value
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @EventSubTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @EventSubTypeId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
