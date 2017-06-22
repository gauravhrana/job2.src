IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PriceScheduleXPriceListDelete') 
BEGIN
	DROP Procedure PriceScheduleXPriceListDelete
END
GO

CREATE Procedure dbo.PriceScheduleXPriceListDelete
(
		@PriceScheduleXPriceListId				INT		= NULL
	,	@PriceScheduleId				INT		= NULL
	,	@PriceListId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'PriceScheduleXPriceList'
)
AS
BEGIN

	DELETE dbo.PriceScheduleXPriceList
	WHERE		PriceScheduleXPriceListId = ISNULL(@PriceScheduleXPriceListId, PriceScheduleXPriceListId)
	AND			PriceScheduleId = ISNULL(@PriceScheduleId, PriceScheduleId)
	AND			PriceListId = ISNULL(@PriceListId, PriceListId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @PriceScheduleXPriceListId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
