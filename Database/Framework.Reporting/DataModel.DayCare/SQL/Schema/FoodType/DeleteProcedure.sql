IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FoodTypeDelete') 
	BEGIN
	DROP Procedure FoodTypeDelete
END
GO

CREATE Procedure dbo.FoodTypeDelete
(
		@FoodTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='FoodType'
)
AS
BEGIN
		DELETE dbo.FoodType
		WHERE	FoodTypeId = @FoodTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @FoodTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
