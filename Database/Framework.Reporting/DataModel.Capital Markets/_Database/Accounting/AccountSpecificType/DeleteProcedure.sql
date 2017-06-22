IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AccountSpecificTypeDelete') 
	BEGIN
	DROP Procedure AccountSpecificTypeDelete
END
GO

CREATE Procedure dbo.AccountSpecificTypeDelete
(
		@AccountSpecificTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AccountSpecificType'
)
AS
BEGIN
		DELETE dbo.AccountSpecificType
		WHERE	AccountSpecificTypeId = @AccountSpecificTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AccountSpecificTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
