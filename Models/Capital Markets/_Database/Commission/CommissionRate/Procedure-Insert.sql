IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionRateInsert') 
BEGIN
	DROP Procedure CommissionRateInsert
END
GO

CREATE Procedure dbo.CommissionRateInsert
(
		@CommissionRateId				INT		= NULL 	OUTPUT 
	,	@ClearingRate				DECIMAL(18, 5)
	,	@ExecutionRate				DECIMAL(18, 5)
	,	@BrokerId				INT
	,	@ExchangeId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CommissionRate'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CommissionRateId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.CommissionRate
	(
			CommissionRateId
		,	ClearingRate
		,	ExecutionRate
		,	BrokerId
		,	ExchangeId
		,	ApplicationId
	)
	VALUES
	(
			@CommissionRateId
		,	@ClearingRate
		,	@ExecutionRate
		,	@BrokerId
		,	@ExchangeId
		,	@ApplicationId
	)

	SELECT @CommissionRateId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CommissionRateId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
