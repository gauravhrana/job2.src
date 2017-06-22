IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='StrategyGroupUpdate') 
BEGIN
	DROP Procedure StrategyGroupUpdate
END
GO

CREATE Procedure dbo.StrategyGroupUpdate
(
		@StrategyGroupId				INT
	,	@FundId				INT
	,	@ClassificationId				INT
	,	@PortfolioId				INT
	,	@ParentStrategyGroupId				INT
	,	@DefaultUSecurityId				INT
	,	@ActiveYN				INT
	,	@OpenDateId				INT
	,	@CloseDateId				INT
	,	@ClosedYN				INT
	,	@ThemeId				INT
	,	@StrategyGroupCode				VARCHAR(500)
	,	@StrategyGroupDesc				VARCHAR(500)
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'StrategyGroup'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.StrategyGroup SET
			FundId				=	@FundId
		,	ClassificationId				=	@ClassificationId
		,	PortfolioId				=	@PortfolioId
		,	ParentStrategyGroupId				=	@ParentStrategyGroupId
		,	DefaultUSecurityId				=	@DefaultUSecurityId
		,	ActiveYN				=	@ActiveYN
		,	OpenDateId				=	@OpenDateId
		,	CloseDateId				=	@CloseDateId
		,	ClosedYN				=	@ClosedYN
		,	ThemeId				=	@ThemeId
		,	StrategyGroupCode				=	@StrategyGroupCode
		,	StrategyGroupDesc				=	@StrategyGroupDesc
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	StrategyGroupId			=   @StrategyGroupId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @StrategyGroupId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
