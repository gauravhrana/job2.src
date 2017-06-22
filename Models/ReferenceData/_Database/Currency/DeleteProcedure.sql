IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CurrencyDelete') 
	BEGIN
	DROP Procedure CurrencyDelete
END
GO

CREATE Procedure dbo.CurrencyDelete
(
		@CurrencyId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Currency'
)
AS
BEGIN
		DELETE dbo.Currency
		WHERE	CurrencyId = @CurrencyId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CurrencyId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
