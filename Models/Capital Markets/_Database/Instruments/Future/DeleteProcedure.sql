IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FutureDelete') 
	BEGIN
	DROP Procedure FutureDelete
END
GO

CREATE Procedure dbo.FutureDelete
(
		@FutureId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Future'
)
AS
BEGIN
		DELETE dbo.Future
		WHERE	FutureId = @FutureId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @FutureId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
