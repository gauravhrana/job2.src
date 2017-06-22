IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CourseUpdate') 
BEGIN
	DROP Procedure CourseUpdate
END
GO

CREATE Procedure dbo.CourseUpdate
(
		@CourseId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@Duration				DECIMAL(18, 5)
	,	@Fees				DECIMAL(18, 5)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Course'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.Course SET
			Name				=	@Name
		,	Description				=	@Description
		,	Duration				=	@Duration
		,	Fees				=	@Fees
		,	SortOrder				=	@SortOrder
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	CourseId			=   @CourseId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CourseId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
