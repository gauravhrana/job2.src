IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PositionInsert') 
BEGIN
	DROP Procedure PositionInsert
END
GO

CREATE Procedure dbo.PositionInsert
(
		@PositionId				INT		= NULL 	OUTPUT 
	,	@InvestmentCode				VARCHAR(500)
	,	@PeriodDate				DATETIME
	,	@CustodianCode				VARCHAR(500)
	,	@StrategyCode				VARCHAR(500)
	,	@AccountCode				VARCHAR(500)
	,	@Quantity				DECIMAL(18, 5)
	,	@CostBasis				DECIMAL(18, 5)
	,	@MarketValue				DECIMAL(18, 5)
	,	@StartMarketValue				DECIMAL(18, 5)
	,	@DeltaAdjustedExposure				DECIMAL(18, 5)
	,	@StartDeltaAdjustedExposure				DECIMAL(18, 5)
	,	@RealizedPnL				DECIMAL(18, 5)
	,	@UnrealizedPnL				DECIMAL(18, 5)
	,	@Mark				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Position'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @PositionId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.Position
	(
			PositionId
		,	InvestmentCode
		,	PeriodDate
		,	CustodianCode
		,	StrategyCode
		,	AccountCode
		,	Quantity
		,	CostBasis
		,	MarketValue
		,	StartMarketValue
		,	DeltaAdjustedExposure
		,	StartDeltaAdjustedExposure
		,	RealizedPnL
		,	UnrealizedPnL
		,	Mark
		,	ApplicationId
	)
	VALUES
	(
			@PositionId
		,	@InvestmentCode
		,	@PeriodDate
		,	@CustodianCode
		,	@StrategyCode
		,	@AccountCode
		,	@Quantity
		,	@CostBasis
		,	@MarketValue
		,	@StartMarketValue
		,	@DeltaAdjustedExposure
		,	@StartDeltaAdjustedExposure
		,	@RealizedPnL
		,	@UnrealizedPnL
		,	@Mark
		,	@ApplicationId
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
