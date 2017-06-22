IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PortfolioXCustodianAccountUpdate') 
BEGIN
	DROP Procedure PortfolioXCustodianAccountUpdate
END
GO

CREATE Procedure dbo.PortfolioXCustodianAccountUpdate
(
		@PortfolioXCustodianAccountId				INT
	,	@CustodianAccountId				INT
	,	@PortfolioId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'PortfolioXCustodianAccount'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.PortfolioXCustodianAccount SET
			CustodianAccountId				=	@CustodianAccountId
		,	PortfolioId				=	@PortfolioId
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	PortfolioXCustodianAccountId			=   @PortfolioXCustodianAccountId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PortfolioXCustodianAccountId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
