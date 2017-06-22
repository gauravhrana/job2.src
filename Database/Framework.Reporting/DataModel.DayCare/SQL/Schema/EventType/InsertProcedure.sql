IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='EventTypeInsert') 
	BEGIN
	DROP Procedure EventTypeInsert
END
GO

CREATE Procedure dbo.EventTypeInsert
(
		@EventTypeId			INT				= NULL 	OUTPUT 
	,	@ApplicationId				INT
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		='EventType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@EventTypeId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.EventType
	(
			EventTypeId
		,	ApplicationId
		,	Name
		,	Description
		,	SortOrder
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@EventTypeId
		,	@ApplicationId
		,	@Name
		,	@Description
		,	@SortOrder
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @EventTypeId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
