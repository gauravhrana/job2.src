IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ProvinceUpdate') 
BEGIN
	DROP Procedure ProvinceUpdate
END
GO

CREATE Procedure dbo.ProvinceUpdate
(
		@ProvinceId				INT
	,	@CountryId				INT
	,	@ProvinceTypeId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Province'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.Province SET
			CountryId				=	@CountryId
		,	ProvinceTypeId				=	@ProvinceTypeId
		,	Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	ProvinceId			=   @ProvinceId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ProvinceId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
