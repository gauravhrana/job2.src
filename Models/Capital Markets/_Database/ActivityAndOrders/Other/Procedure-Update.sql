IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxOtherUpdate') 
BEGIN
	DROP Procedure TxOtherUpdate
END
GO

CREATE Procedure dbo.TxOtherUpdate
(
		@TxOtherId				INT
	,	@TransactionEventId				INT
	,	@FundStructureId				INT
	,	@CashSourceId				INT
	,	@StrategyId				INT
	,	@GenericLegId				INT
	,	@DistributionParentId				INT
	,	@SettlementTypeId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxOther'
)
AS
BEGIN
			TransactionEventId				=	@TransactionEventId
		,	FundStructureId				=	@FundStructureId
		,	CashSourceId				=	@CashSourceId
		,	StrategyId				=	@StrategyId
		,	GenericLegId				=	@GenericLegId
		,	DistributionParentId				=	@DistributionParentId
		,	SettlementTypeId				=	@SettlementTypeId
	WHERE	TxOtherId			=   @TxOtherId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TxOtherId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
