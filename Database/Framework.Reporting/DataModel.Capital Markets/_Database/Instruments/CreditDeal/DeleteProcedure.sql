IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CreditDealDelete') 
	BEGIN
	DROP Procedure CreditDealDelete
END
GO

CREATE Procedure dbo.CreditDealDelete
(
		@CreditDealId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='CreditDeal'
)
AS
BEGIN
		DELETE dbo.CreditDeal
		WHERE	CreditDealId = @CreditDealId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CreditDealId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
