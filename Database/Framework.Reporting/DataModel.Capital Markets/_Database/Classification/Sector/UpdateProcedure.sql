IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SectorUpdate') 
BEGIN
	DROP Procedure SectorUpdate
END
GO

CREATE Procedure dbo.SectorUpdate
(
		@SectorId				INT
	,	@Code				VARCHAR(500)
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Sector'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.Sector SET
			Code				=	@Code
		,	Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	SectorId			=   @SectorId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SectorId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
