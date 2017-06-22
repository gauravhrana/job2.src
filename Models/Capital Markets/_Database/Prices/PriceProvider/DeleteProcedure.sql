IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PriceProviderDelete') 
	BEGIN
	DROP Procedure PriceProviderDelete
END
GO

CREATE Procedure dbo.PriceProviderDelete
(
		@PriceProviderId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='PriceProvider'
)
AS
BEGIN
		DELETE dbo.PriceProvider
		WHERE	PriceProviderId = @PriceProviderId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PriceProviderId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
