IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='LegalEntityDelete') 
BEGIN
	DROP Procedure LegalEntityDelete
END
GO

CREATE Procedure dbo.LegalEntityDelete
(
		@LegalEntityId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'LegalEntity'
)
AS
BEGIN

	DELETE dbo.LegalEntity
	WHERE	LegalEntityId = @LegalEntityId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @LegalEntityId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
