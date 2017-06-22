IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RatingInsert') 
BEGIN
	DROP Procedure RatingInsert
END
GO

CREATE Procedure dbo.RatingInsert
(
		@RatingId				INT		= NULL 	OUTPUT 
	,	@Date				DATETIME
	,	@Analyst				VARCHAR(500)
	,	@Rating				DECIMAL(18, 5)
	,	@Notes				VARCHAR(500)
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Rating'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@RatingId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.Rating
	(
			RatingId
		,	Date
		,	Analyst
		,	Rating
		,	Notes
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@RatingId
		,	@Date
		,	@Analyst
		,	@Rating
		,	@Notes
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @RatingId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @RatingId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
