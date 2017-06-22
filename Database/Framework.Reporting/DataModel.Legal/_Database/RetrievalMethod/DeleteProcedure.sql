IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RetrievalMethodDelete') 
BEGIN
	DROP Procedure RetrievalMethodDelete
END
GO

CREATE Procedure dbo.RetrievalMethodDelete
(
		@RetrievalMethodId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'RetrievalMethod'
)
AS
BEGIN

	DELETE dbo.RetrievalMethod
	WHERE	RetrievalMethodId = @RetrievalMethodId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @RetrievalMethodId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
