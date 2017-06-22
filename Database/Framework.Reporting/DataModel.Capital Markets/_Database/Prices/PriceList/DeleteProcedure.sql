IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PriceListDelete') 
	BEGIN
	DROP Procedure PriceListDelete
END
GO

CREATE Procedure dbo.PriceListDelete
(
		@PriceListId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='PriceList'
)
AS
BEGIN
		DELETE dbo.PriceList
		WHERE	PriceListId = @PriceListId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PriceListId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
