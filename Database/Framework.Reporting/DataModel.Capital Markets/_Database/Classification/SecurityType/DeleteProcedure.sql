IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityTypeDelete') 
BEGIN
	DROP Procedure SecurityTypeDelete
END
GO

CREATE Procedure dbo.SecurityTypeDelete
(
		@SecurityTypeId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'SecurityType'
)
AS
BEGIN

	DELETE dbo.SecurityType

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SecurityTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
