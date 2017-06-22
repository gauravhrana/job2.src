IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AssetBackedUpdate') 
	BEGIN
	DROP Procedure AssetBackedUpdate
END
GO

CREATE Procedure dbo.AssetBackedUpdate
(
		@AssetBackedId			INT
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'AssetBacked'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT
	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE dbo.AssetBacked SET
		Name				=	@Name
	,	Description				=	@Description
	,	SortOrder				=	@SortOrder
	,	UpdatedDate				=	@UpdatedDate
	,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE
		AssetBackedId			=   @AssetBackedId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType		= @SystemEntityType
	,	@EntityKey				= @AssetBackedId
	,	@AuditAction			= 'Update'
	,	@CreatedDate			= @AuditDate
	,	@CreatedByPersonId		= @AuditId

END
GO
