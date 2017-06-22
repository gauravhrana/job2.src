IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ExchangeDelete') 
	BEGIN
	DROP Procedure ExchangeDelete
END
GO

CREATE Procedure dbo.ExchangeDelete
(
		@ExchangeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Exchange'
)
AS
BEGIN
		DELETE dbo.Exchange
		WHERE	ExchangeId = @ExchangeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ExchangeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
