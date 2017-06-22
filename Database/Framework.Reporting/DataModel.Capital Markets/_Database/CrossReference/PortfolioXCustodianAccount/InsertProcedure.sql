IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PortfolioXCustodianAccountInsert') 
BEGIN
	DROP Procedure PortfolioXCustodianAccountInsert
END
GO

CREATE Procedure dbo.PortfolioXCustodianAccountInsert
(
		@PortfolioXCustodianAccountId				INT		= NULL 	OUTPUT 
	,	@CustodianAccountId				INT
	,	@PortfolioId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'PortfolioXCustodianAccount'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@PortfolioXCustodianAccountId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.PortfolioXCustodianAccount
	(
			PortfolioXCustodianAccountId
		,	CustodianAccountId
		,	PortfolioId
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@PortfolioXCustodianAccountId
		,	@CustodianAccountId
		,	@PortfolioId
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @PortfolioXCustodianAccountId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PortfolioXCustodianAccountId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
