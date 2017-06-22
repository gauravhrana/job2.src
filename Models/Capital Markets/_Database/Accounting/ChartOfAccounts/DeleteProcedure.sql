IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ChartOfAccountsDelete') 
	BEGIN
	DROP Procedure ChartOfAccountsDelete
END
GO

CREATE Procedure dbo.ChartOfAccountsDelete
(
		@ChartOfAccountsId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='ChartOfAccounts'
)
AS
BEGIN
		DELETE dbo.ChartOfAccounts
		WHERE	ChartOfAccountsId = @ChartOfAccountsId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ChartOfAccountsId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
