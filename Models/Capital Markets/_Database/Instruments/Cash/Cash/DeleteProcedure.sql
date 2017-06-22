IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CashDelete') 
	BEGIN
	DROP Procedure CashDelete
END
GO

CREATE Procedure dbo.CashDelete
(
		@CashId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Cash'
)
AS
BEGIN
		DELETE dbo.Cash
		WHERE	CashId = @CashId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CashId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
