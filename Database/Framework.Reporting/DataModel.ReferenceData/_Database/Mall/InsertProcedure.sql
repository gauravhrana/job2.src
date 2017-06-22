IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MallInsert') 
	BEGIN
	DROP Procedure MallInsert
END
GO

CREATE Procedure dbo.MallInsert
(
		@MallId			INT				= NULL 	OUTPUT 
	,	@ApplicationId				INT
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		='Mall'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@MallId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.Mall
	(
			MallId
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
			@MallId
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
		,	@EntityKey			= @MallId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
