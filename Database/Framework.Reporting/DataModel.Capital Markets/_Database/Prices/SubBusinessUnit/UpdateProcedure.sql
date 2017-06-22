IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SubBusinessUnitUpdate') 
BEGIN
	DROP Procedure SubBusinessUnitUpdate
END
GO

CREATE Procedure dbo.SubBusinessUnitUpdate
(
		@SubBusinessUnitId				INT
	,	@FundId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'SubBusinessUnit'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.SubBusinessUnit SET
			FundId				=	@FundId
		,	Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	SubBusinessUnitId			=   @SubBusinessUnitId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SubBusinessUnitId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
