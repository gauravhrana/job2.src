IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MealDetailDelete') 
	BEGIN
	DROP Procedure MealDetailDelete
END
GO

CREATE Procedure dbo.MealDetailDelete
(
		@MealDetailId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='MealDetail'
)
AS
BEGIN
		DELETE dbo.MealDetail
		WHERE	MealDetailId = @MealDetailId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @MealDetailId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
