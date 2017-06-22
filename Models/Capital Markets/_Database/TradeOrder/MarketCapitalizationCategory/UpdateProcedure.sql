IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MarketCapitalizationCategoryUpdate') 
	BEGIN
	DROP Procedure MarketCapitalizationCategoryUpdate
END
GO

CREATE Procedure dbo.MarketCapitalizationCategoryUpdate
(
		@MarketCapitalizationCategoryId			INT
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'MarketCapitalizationCategory'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT
	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE dbo.MarketCapitalizationCategory SET
		Name				=	@Name
	,	Description				=	@Description
	,	SortOrder				=	@SortOrder
	,	UpdatedDate				=	@UpdatedDate
	,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE
		MarketCapitalizationCategoryId			=   @MarketCapitalizationCategoryId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType		= @SystemEntityType
	,	@EntityKey				= @MarketCapitalizationCategoryId
	,	@AuditAction			= 'Update'
	,	@CreatedDate			= @AuditDate
	,	@CreatedByPersonId		= @AuditId

END
GO
