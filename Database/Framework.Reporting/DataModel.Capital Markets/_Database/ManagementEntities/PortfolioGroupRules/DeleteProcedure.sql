IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PortfolioGroupRulesDelete') 
BEGIN
	DROP Procedure PortfolioGroupRulesDelete
END
GO

CREATE Procedure dbo.PortfolioGroupRulesDelete
(
		@PortfolioGroupRulesId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'PortfolioGroupRules'
)
AS
BEGIN

	DELETE dbo.PortfolioGroupRules
	WHERE	PortfolioGroupRulesId = @PortfolioGroupRulesId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @PortfolioGroupRulesId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
