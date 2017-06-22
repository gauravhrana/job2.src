IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AccountAdministratorDelete') 
	BEGIN
	DROP Procedure AccountAdministratorDelete
END
GO

CREATE Procedure dbo.AccountAdministratorDelete
(
		@AccountAdministratorId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AccountAdministrator'
)
AS
BEGIN
		DELETE dbo.AccountAdministrator
		WHERE	AccountAdministratorId = @AccountAdministratorId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AccountAdministratorId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
