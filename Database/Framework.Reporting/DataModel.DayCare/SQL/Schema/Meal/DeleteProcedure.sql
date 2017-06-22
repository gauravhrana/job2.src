IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MealDelete') 
	BEGIN
	DROP Procedure MealDelete
END
GO

CREATE Procedure dbo.MealDelete
(
		@MealId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Meal'
)
AS
BEGIN
		DELETE dbo.Meal
		WHERE	MealId = @MealId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @MealId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
