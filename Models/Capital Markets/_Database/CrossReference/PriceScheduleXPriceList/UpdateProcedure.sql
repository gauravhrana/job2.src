IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PriceScheduleXPriceListUpdate') 
BEGIN
	DROP Procedure PriceScheduleXPriceListUpdate
END
GO

CREATE Procedure dbo.PriceScheduleXPriceListUpdate
(
		@PriceScheduleXPriceListId				INT
	,	@PriceScheduleId				INT
	,	@PriceListId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'PriceScheduleXPriceList'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.PriceScheduleXPriceList SET
			PriceScheduleId				=	@PriceScheduleId
		,	PriceListId				=	@PriceListId
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	PriceScheduleXPriceListId			=   @PriceScheduleXPriceListId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PriceScheduleXPriceListId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
