IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='StrategyGroupInsert') 
BEGIN
	DROP Procedure StrategyGroupInsert
END
GO

CREATE Procedure dbo.StrategyGroupInsert
(
		@StrategyGroupId				INT		= NULL 	OUTPUT 
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
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'StrategyGroup'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@StrategyGroupId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.StrategyGroup
	(
			StrategyGroupId
		,	FundId
		,	ClassificationId
		,	PortfolioId
		,	ParentStrategyGroupId
		,	DefaultUSecurityId
		,	ActiveYN
		,	OpenDateId
		,	CloseDateId
		,	ClosedYN
		,	ThemeId
		,	StrategyGroupCode
		,	StrategyGroupDesc
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@StrategyGroupId
		,	@FundId
		,	@ClassificationId
		,	@PortfolioId
		,	@ParentStrategyGroupId
		,	@DefaultUSecurityId
		,	@ActiveYN
		,	@OpenDateId
		,	@CloseDateId
		,	@ClosedYN
		,	@ThemeId
		,	@StrategyGroupCode
		,	@StrategyGroupDesc
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @StrategyGroupId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @StrategyGroupId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
