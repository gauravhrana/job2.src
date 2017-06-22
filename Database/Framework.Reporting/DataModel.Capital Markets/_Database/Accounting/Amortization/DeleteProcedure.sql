IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AmortizationDelete') 
	BEGIN
	DROP Procedure AmortizationDelete
END
GO

CREATE Procedure dbo.AmortizationDelete
(
		@AmortizationId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Amortization'
)
AS
BEGIN
		DELETE dbo.Amortization
		WHERE	AmortizationId = @AmortizationId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AmortizationId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
