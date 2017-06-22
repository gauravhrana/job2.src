IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RatingDelete') 
BEGIN
	DROP Procedure RatingDelete
END
GO

CREATE Procedure dbo.RatingDelete
(
		@RatingId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Rating'
)
AS
BEGIN

	DELETE dbo.Rating
	WHERE	RatingId = @RatingId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @RatingId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
