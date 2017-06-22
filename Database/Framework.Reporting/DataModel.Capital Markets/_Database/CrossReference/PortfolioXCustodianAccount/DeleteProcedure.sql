IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PortfolioXCustodianAccountDelete') 
BEGIN
	DROP Procedure PortfolioXCustodianAccountDelete
END
GO

CREATE Procedure dbo.PortfolioXCustodianAccountDelete
(
		@PortfolioXCustodianAccountId				INT		= NULL
	,	@CustodianAccountId				INT		= NULL
	,	@PortfolioId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'PortfolioXCustodianAccount'
)
AS
BEGIN

	DELETE dbo.PortfolioXCustodianAccount
	WHERE		PortfolioXCustodianAccountId = ISNULL(@PortfolioXCustodianAccountId, PortfolioXCustodianAccountId)
	AND			CustodianAccountId = ISNULL(@CustodianAccountId, CustodianAccountId)
	AND			PortfolioId = ISNULL(@PortfolioId, PortfolioId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @PortfolioXCustodianAccountId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
