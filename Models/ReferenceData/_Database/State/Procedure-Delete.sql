IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='StateDelete') 
BEGIN
	DROP Procedure StateDelete
END
GO

CREATE Procedure dbo.StateDelete
(
		@StateId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'State'
)
AS
BEGIN

	DELETE dbo.State
	WHERE	StateId = @StateId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @StateId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
