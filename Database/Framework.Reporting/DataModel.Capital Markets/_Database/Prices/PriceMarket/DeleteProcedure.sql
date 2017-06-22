IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PriceMarketDelete') 
	BEGIN
	DROP Procedure PriceMarketDelete
END
GO

CREATE Procedure dbo.PriceMarketDelete
(
		@PriceMarketId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='PriceMarket'
)
AS
BEGIN
		DELETE dbo.PriceMarket
		WHERE	PriceMarketId = @PriceMarketId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PriceMarketId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
