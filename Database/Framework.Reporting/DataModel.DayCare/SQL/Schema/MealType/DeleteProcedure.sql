IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MealTypeDelete') 
	BEGIN
	DROP Procedure MealTypeDelete
END
GO

CREATE Procedure dbo.MealTypeDelete
(
		@MealTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='MealType'
)
AS
BEGIN
		DELETE dbo.MealType
		WHERE	MealTypeId = @MealTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @MealTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
