IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CompanyDealTypeUpdate') 
BEGIN
	DROP Procedure CompanyDealTypeUpdate
END
GO

CREATE Procedure dbo.CompanyDealTypeUpdate
(
		@CompanyDealTypeId				INT
	,	@FundId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CompanyDealType'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.CompanyDealType SET
			FundId				=	@FundId
		,	Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	CompanyDealTypeId			=   @CompanyDealTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CompanyDealTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
