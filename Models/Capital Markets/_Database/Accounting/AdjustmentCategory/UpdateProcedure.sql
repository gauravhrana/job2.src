IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AdjustmentCategoryUpdate') 
BEGIN
	DROP Procedure AdjustmentCategoryUpdate
END
GO

CREATE Procedure dbo.AdjustmentCategoryUpdate
(
		@AdjustmentCategoryId				INT
	,	@Code				VARCHAR(500)
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'AdjustmentCategory'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.AdjustmentCategory SET
			Code				=	@Code
		,	Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	AdjustmentCategoryId			=   @AdjustmentCategoryId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AdjustmentCategoryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
