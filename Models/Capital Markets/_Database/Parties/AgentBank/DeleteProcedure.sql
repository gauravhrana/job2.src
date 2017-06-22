IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AgentBankDelete') 
	BEGIN
	DROP Procedure AgentBankDelete
END
GO

CREATE Procedure dbo.AgentBankDelete
(
		@AgentBankId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AgentBank'
)
AS
BEGIN
		DELETE dbo.AgentBank
		WHERE	AgentBankId = @AgentBankId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AgentBankId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
