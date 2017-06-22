IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MarketCapitalizationCategoryDelete') 
	BEGIN
	DROP Procedure MarketCapitalizationCategoryDelete
END
GO

CREATE Procedure dbo.MarketCapitalizationCategoryDelete
(
		@MarketCapitalizationCategoryId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='MarketCapitalizationCategory'
)
AS
BEGIN
		DELETE dbo.MarketCapitalizationCategory
		WHERE	MarketCapitalizationCategoryId = @MarketCapitalizationCategoryId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @MarketCapitalizationCategoryId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
