IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PositionInsert') 
BEGIN
	DROP Procedure PositionInsert
END
GO

CREATE Procedure dbo.PositionInsert
(
		@PositionId				INT		= NULL 	OUTPUT 
	,	@CustodianCode				VARCHAR(500)
	,	@StrategyCode				VARCHAR(500)
	,	@AccountCode				VARCHAR(500)
	,	@InvestmentCode				VARCHAR(500)
	,	@Quantity				DECIMAL(18, 5)
	,	@CostBasis				DECIMAL(18, 5)
	,	@MarketValue				DECIMAL(18, 5)
	,	@StartMarketValue				DECIMAL(18, 5)
	,	@DeltaAdjustedExposure				DECIMAL(18, 5)
	,	@StartDeltaAdjustedExposure				DECIMAL(18, 5)
	,	@RealizedPnL				DECIMAL(18, 5)
	,	@UnrealizedPnL				DECIMAL(18, 5)
	,	@PeriodDate				DATETIME
	,	@Mark				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Position'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@PositionId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.Position
	(
			PositionId
		,	CustodianCode
		,	StrategyCode
		,	AccountCode
		,	InvestmentCode
		,	Quantity
		,	CostBasis
		,	MarketValue
		,	StartMarketValue
		,	DeltaAdjustedExposure
		,	StartDeltaAdjustedExposure
		,	RealizedPnL
		,	UnrealizedPnL
		,	PeriodDate
		,	Mark
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@PositionId
		,	@CustodianCode
		,	@StrategyCode
		,	@AccountCode
		,	@InvestmentCode
		,	@Quantity
		,	@CostBasis
		,	@MarketValue
		,	@StartMarketValue
		,	@DeltaAdjustedExposure
		,	@StartDeltaAdjustedExposure
		,	@RealizedPnL
		,	@UnrealizedPnL
		,	@PeriodDate
		,	@Mark
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @PositionId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PositionId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
