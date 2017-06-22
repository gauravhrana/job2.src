IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PortfolioDelete') 
BEGIN
	DROP Procedure PortfolioDelete
END
GO

CREATE Procedure dbo.PortfolioDelete
(
		@PortfolioId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Portfolio'
)
AS
BEGIN

	DELETE dbo.Portfolio
	WHERE	PortfolioId = @PortfolioId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @PortfolioId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
