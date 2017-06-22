IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AdjustmentCategoryDelete') 
BEGIN
	DROP Procedure AdjustmentCategoryDelete
END
GO

CREATE Procedure dbo.AdjustmentCategoryDelete
(
		@AdjustmentCategoryId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'AdjustmentCategory'
)
AS
BEGIN

	DELETE dbo.AdjustmentCategory
	WHERE	AdjustmentCategoryId = @AdjustmentCategoryId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @AdjustmentCategoryId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
