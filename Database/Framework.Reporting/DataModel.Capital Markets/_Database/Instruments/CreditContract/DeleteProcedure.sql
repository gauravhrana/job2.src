IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CreditContractDelete') 
	BEGIN
	DROP Procedure CreditContractDelete
END
GO

CREATE Procedure dbo.CreditContractDelete
(
		@CreditContractId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='CreditContract'
)
AS
BEGIN
		DELETE dbo.CreditContract
		WHERE	CreditContractId = @CreditContractId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CreditContractId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
