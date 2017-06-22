IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderItemUpdate') 
BEGIN
	DROP Procedure OrderItemUpdate
END
GO

CREATE Procedure dbo.OrderItemUpdate
(
		@OrderItemId				INT
	,	@OrderId				INT
	,	@SecurityCode				VARCHAR(500)
	,	@Quantity				DECIMAL(18, 5)
	,	@QuantityFilled				DECIMAL(18, 5)
	,	@QuantityOriginal				DECIMAL(18, 5)
	,	@PriceLimit				DECIMAL(18, 5)
	,	@StrategyCode				VARCHAR(500)
	,	@StrategyGroupCode				VARCHAR(500)
	,	@ClassificationCode				VARCHAR(500)
	,	@BbergCode				VARCHAR(500)
	,	@Notes				VARCHAR(500)
	,	@AvgPrice				DECIMAL(18, 5)
	,	@RefPrice				DECIMAL(18, 5)
	,	@TargetBps				DECIMAL(18, 5)
	,	@AutoGeneratedNotes				VARCHAR(500)
	,	@AutoPercentTraded				DECIMAL(18, 5)
	,	@PositionSizeChange				DECIMAL(18, 5)
	,	@SubmissionType				VARCHAR(500)
	,	@PrimaryBrokerCode				VARCHAR(500)
	,	@ExecutingBrokerCode				VARCHAR(500)
	,	@RoutingDestination				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@TotalOrderPercent				DECIMAL(18, 5)
	,	@EventDate				DATETIME
	,	@AutoOrderResultTypeId				INT
	,	@BbergUniqueId				INT
	,	@LastOrderStatusId				INT
	,	@LastModifiedOn				DATETIME
	,	@ExpireOn				DATETIME
	,	@RefFxRate				INT
	,	@OrderRequestId				INT
	,	@OrderActionId				INT
	,	@OrderTypeId				INT
	,	@StrategyId				INT
	,	@SecurityId				INT
	,	@PortfolioId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderItem'
)
AS
BEGIN

	UPDATE	dbo.OrderItem
	SET
			OrderId				=	@OrderId
		,	SecurityCode				=	@SecurityCode
		,	Quantity				=	@Quantity
		,	QuantityFilled				=	@QuantityFilled
		,	QuantityOriginal				=	@QuantityOriginal
		,	PriceLimit				=	@PriceLimit
		,	StrategyCode				=	@StrategyCode
		,	StrategyGroupCode				=	@StrategyGroupCode
		,	ClassificationCode				=	@ClassificationCode
		,	BbergCode				=	@BbergCode
		,	Notes				=	@Notes
		,	AvgPrice				=	@AvgPrice
		,	RefPrice				=	@RefPrice
		,	TargetBps				=	@TargetBps
		,	AutoGeneratedNotes				=	@AutoGeneratedNotes
		,	AutoPercentTraded				=	@AutoPercentTraded
		,	PositionSizeChange				=	@PositionSizeChange
		,	SubmissionType				=	@SubmissionType
		,	PrimaryBrokerCode				=	@PrimaryBrokerCode
		,	ExecutingBrokerCode				=	@ExecutingBrokerCode
		,	RoutingDestination				=	@RoutingDestination
		,	Description				=	@Description
		,	TotalOrderPercent				=	@TotalOrderPercent
		,	EventDate				=	@EventDate
		,	AutoOrderResultTypeId				=	@AutoOrderResultTypeId
		,	BbergUniqueId				=	@BbergUniqueId
		,	LastOrderStatusId				=	@LastOrderStatusId
		,	LastModifiedOn				=	@LastModifiedOn
		,	ExpireOn				=	@ExpireOn
		,	RefFxRate				=	@RefFxRate
		,	OrderRequestId				=	@OrderRequestId
		,	OrderActionId				=	@OrderActionId
		,	OrderTypeId				=	@OrderTypeId
		,	StrategyId				=	@StrategyId
		,	SecurityId				=	@SecurityId
		,	PortfolioId				=	@PortfolioId
	WHERE	OrderItemId			=   @OrderItemId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @OrderItemId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
