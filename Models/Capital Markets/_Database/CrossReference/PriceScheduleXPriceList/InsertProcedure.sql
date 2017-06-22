IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PriceScheduleXPriceListInsert') 
BEGIN
	DROP Procedure PriceScheduleXPriceListInsert
END
GO

CREATE Procedure dbo.PriceScheduleXPriceListInsert
(
		@PriceScheduleXPriceListId				INT		= NULL 	OUTPUT 
	,	@PriceScheduleId				INT
	,	@PriceListId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'PriceScheduleXPriceList'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@PriceScheduleXPriceListId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.PriceScheduleXPriceList
	(
			PriceScheduleXPriceListId
		,	PriceScheduleId
		,	PriceListId
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@PriceScheduleXPriceListId
		,	@PriceScheduleId
		,	@PriceListId
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @PriceScheduleXPriceListId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PriceScheduleXPriceListId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
