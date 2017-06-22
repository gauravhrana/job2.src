IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CourseInsert') 
BEGIN
	DROP Procedure CourseInsert
END
GO

CREATE Procedure dbo.CourseInsert
(
		@CourseId				INT		= NULL 	OUTPUT 
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@Duration				DECIMAL(18, 5)
	,	@Fees				DECIMAL(18, 5)
	,	@SortOrder				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Course'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@CourseId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.Course
	(
			CourseId
		,	Name
		,	Description
		,	Duration
		,	Fees
		,	SortOrder
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@CourseId
		,	@Name
		,	@Description
		,	@Duration
		,	@Fees
		,	@SortOrder
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @CourseId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CourseId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
