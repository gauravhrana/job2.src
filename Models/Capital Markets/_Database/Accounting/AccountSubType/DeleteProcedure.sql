IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AccountSubTypeDelete') 
	BEGIN
	DROP Procedure AccountSubTypeDelete
END
GO

CREATE Procedure dbo.AccountSubTypeDelete
(
		@AccountSubTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AccountSubType'
)
AS
BEGIN
		DELETE dbo.AccountSubType
		WHERE	AccountSubTypeId = @AccountSubTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AccountSubTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
