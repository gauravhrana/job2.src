IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='InvestmentPricesDelete') 
	BEGIN
	DROP Procedure InvestmentPricesDelete
END
GO

CREATE Procedure dbo.InvestmentPricesDelete
(
		@InvestmentPricesId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='InvestmentPrices'
)
AS
BEGIN
		DELETE dbo.InvestmentPrices
		WHERE	InvestmentPricesId = @InvestmentPricesId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @InvestmentPricesId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
