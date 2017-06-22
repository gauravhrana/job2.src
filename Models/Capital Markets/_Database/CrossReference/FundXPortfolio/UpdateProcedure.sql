IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FundXPortfolioUpdate') 
BEGIN
	DROP Procedure FundXPortfolioUpdate
END
GO

CREATE Procedure dbo.FundXPortfolioUpdate
(
		@FundXPortfolioId				INT
	,	@FundId				INT
	,	@PortfolioId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'FundXPortfolio'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.FundXPortfolio SET
			FundId				=	@FundId
		,	PortfolioId				=	@PortfolioId
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	FundXPortfolioId			=   @FundXPortfolioId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FundXPortfolioId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
