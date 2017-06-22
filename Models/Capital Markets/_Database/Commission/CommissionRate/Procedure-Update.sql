IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionRateUpdate') 
BEGIN
	DROP Procedure CommissionRateUpdate
END
GO

CREATE Procedure dbo.CommissionRateUpdate
(
		@CommissionRateId				INT
	,	@ClearingRate				DECIMAL(18, 5)
	,	@ExecutionRate				DECIMAL(18, 5)
	,	@BrokerId				INT
	,	@ExchangeId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CommissionRate'
)
AS
BEGIN

	UPDATE	dbo.CommissionRate
	SET
			ClearingRate				=	@ClearingRate
		,	ExecutionRate				=	@ExecutionRate
		,	BrokerId				=	@BrokerId
		,	ExchangeId				=	@ExchangeId
	WHERE	CommissionRateId			=   @CommissionRateId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CommissionRateId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
