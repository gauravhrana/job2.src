IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FundXPortfolioDelete') 
BEGIN
	DROP Procedure FundXPortfolioDelete
END
GO

CREATE Procedure dbo.FundXPortfolioDelete
(
		@FundXPortfolioId				INT		= NULL
	,	@FundId				INT		= NULL
	,	@PortfolioId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'FundXPortfolio'
)
AS
BEGIN

	DELETE dbo.FundXPortfolio
	WHERE		FundXPortfolioId = ISNULL(@FundXPortfolioId, FundXPortfolioId)
	AND			FundId = ISNULL(@FundId, FundId)
	AND			PortfolioId = ISNULL(@PortfolioId, PortfolioId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @FundXPortfolioId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
