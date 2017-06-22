IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXInvestmentIdentifierDelete') 
BEGIN
	DROP Procedure SecurityXInvestmentIdentifierDelete
END
GO

CREATE Procedure dbo.SecurityXInvestmentIdentifierDelete
(
		@SecurityXInvestmentIdentifierId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'SecurityXInvestmentIdentifier'
)
AS
BEGIN

	DELETE dbo.SecurityXInvestmentIdentifier
	WHERE	SecurityXInvestmentIdentifierId = @SecurityXInvestmentIdentifierId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SecurityXInvestmentIdentifierId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
