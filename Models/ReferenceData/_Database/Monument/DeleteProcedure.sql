IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MonumentDelete') 
	BEGIN
	DROP Procedure MonumentDelete
END
GO

CREATE Procedure dbo.MonumentDelete
(
		@MonumentId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Monument'
)
AS
BEGIN
		DELETE dbo.Monument
		WHERE	MonumentId = @MonumentId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @MonumentId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
