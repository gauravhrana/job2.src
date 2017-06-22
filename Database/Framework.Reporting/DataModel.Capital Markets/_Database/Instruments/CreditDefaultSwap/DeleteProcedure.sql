IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CreditDefaultSwapDelete') 
	BEGIN
	DROP Procedure CreditDefaultSwapDelete
END
GO

CREATE Procedure dbo.CreditDefaultSwapDelete
(
		@CreditDefaultSwapId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='CreditDefaultSwap'
)
AS
BEGIN
		DELETE dbo.CreditDefaultSwap
		WHERE	CreditDefaultSwapId = @CreditDefaultSwapId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CreditDefaultSwapId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
