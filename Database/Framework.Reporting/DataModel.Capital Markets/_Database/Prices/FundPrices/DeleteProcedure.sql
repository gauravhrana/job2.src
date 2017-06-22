IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FundPricesDelete') 
	BEGIN
	DROP Procedure FundPricesDelete
END
GO

CREATE Procedure dbo.FundPricesDelete
(
		@FundPricesId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='FundPrices'
)
AS
BEGIN
		DELETE dbo.FundPrices
		WHERE	FundPricesId = @FundPricesId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @FundPricesId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
