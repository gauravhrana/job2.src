IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityDelete') 
BEGIN
	DROP Procedure SecurityDelete
END
GO

CREATE Procedure dbo.SecurityDelete
(
		@SecurityId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Security'
)
AS
BEGIN

	DELETE dbo.Security

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SecurityId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
