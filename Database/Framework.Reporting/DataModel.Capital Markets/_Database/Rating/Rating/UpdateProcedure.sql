IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RatingUpdate') 
	BEGIN
	DROP Procedure RatingUpdate
END
GO

CREATE Procedure dbo.RatingUpdate
(
		@RatingId			INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Rating'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT
	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE dbo.Rating SET
		Date				=	@Date
	,	Analyst				=	@Analyst
	,	Rating				=	@Rating
	,	Notes				=	@Notes
	,	UpdatedDate				=	@UpdatedDate
	,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE
		RatingId			=   @RatingId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType		= @SystemEntityType
	,	@EntityKey				= @RatingId
	,	@AuditAction			= 'Update'
	,	@CreatedDate			= @AuditDate
	,	@CreatedByPersonId		= @AuditId

END
GO
