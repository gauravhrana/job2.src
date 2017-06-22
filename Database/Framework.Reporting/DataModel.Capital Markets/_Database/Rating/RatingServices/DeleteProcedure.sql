IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RatingServicesDelete') 
	BEGIN
	DROP Procedure RatingServicesDelete
END
GO

CREATE Procedure dbo.RatingServicesDelete
(
		@RatingServicesId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='RatingServices'
)
AS
BEGIN
		DELETE dbo.RatingServices
		WHERE	RatingServicesId = @RatingServicesId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @RatingServicesId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
