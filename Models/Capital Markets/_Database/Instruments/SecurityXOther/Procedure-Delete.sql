IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXOtherDelete') 
BEGIN
	DROP Procedure SecurityXOtherDelete
END
GO

CREATE Procedure dbo.SecurityXOtherDelete
(
		@SecurityXOtherId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'SecurityXOther'
)
AS
BEGIN

	DELETE dbo.SecurityXOther
	WHERE	SecurityXOtherId = @SecurityXOtherId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SecurityXOtherId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
