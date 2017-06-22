IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PositionUpdate') 
BEGIN
	DROP Procedure PositionUpdate
END
GO

CREATE Procedure dbo.PositionUpdate
(
		@PositionId				INT
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
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Position'
)
AS
BEGIN

	UPDATE	dbo.Position
	SET
			InvestmentCode				=	@InvestmentCode
		,	PeriodDate				=	@PeriodDate
		,	CustodianCode				=	@CustodianCode
		,	StrategyCode				=	@StrategyCode
		,	AccountCode				=	@AccountCode
		,	Quantity				=	@Quantity
		,	CostBasis				=	@CostBasis
		,	MarketValue				=	@MarketValue
		,	StartMarketValue				=	@StartMarketValue
		,	DeltaAdjustedExposure				=	@DeltaAdjustedExposure
		,	StartDeltaAdjustedExposure				=	@StartDeltaAdjustedExposure
		,	RealizedPnL				=	@RealizedPnL
		,	UnrealizedPnL				=	@UnrealizedPnL
		,	Mark				=	@Mark
	WHERE	PositionId			=   @PositionId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PositionId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
