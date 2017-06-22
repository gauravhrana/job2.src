IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxOtherInsert') 
BEGIN
	DROP Procedure TxOtherInsert
END
GO

CREATE Procedure dbo.TxOtherInsert
(
		@TxOtherId				INT		= NULL 	OUTPUT 
	,	@TransactionEventId				INT
	,	@FundStructureId				INT
	,	@CashSourceId				INT
	,	@StrategyId				INT
	,	@GenericLegId				INT
	,	@DistributionParentId				INT
	,	@SettlementTypeId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxOther'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TxOtherId Output, @AuditId


	INSERT INTO dbo.TxOther
	(
			TxOtherId
		,	TransactionEventId
		,	FundStructureId
		,	CashSourceId
		,	StrategyId
		,	GenericLegId
		,	DistributionParentId
		,	SettlementTypeId
		,	ApplicationId
	)
	VALUES
	(
			@TxOtherId
		,	@TransactionEventId
		,	@FundStructureId
		,	@CashSourceId
		,	@StrategyId
		,	@GenericLegId
		,	@DistributionParentId
		,	@SettlementTypeId
		,	@ApplicationId
	)

	SELECT @TxOtherId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TxOtherId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
