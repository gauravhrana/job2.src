IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='DiaperStatusInsert') 
	BEGIN
	DROP Procedure DiaperStatusInsert
END
GO

CREATE Procedure dbo.DiaperStatusInsert
(
		@DiaperStatusId			INT				= NULL 	OUTPUT 
	,	@ApplicationId				INT
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		='DiaperStatus'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@DiaperStatusId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.DiaperStatus
	(
			DiaperStatusId
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
			@DiaperStatusId
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
		,	@EntityKey			= @DiaperStatusId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
