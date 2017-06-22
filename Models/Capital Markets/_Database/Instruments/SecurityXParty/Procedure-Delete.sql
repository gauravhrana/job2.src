IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXPartyDelete') 
BEGIN
	DROP Procedure SecurityXPartyDelete
END
GO

CREATE Procedure dbo.SecurityXPartyDelete
(
		@SecurityXPartyId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'SecurityXParty'
)
AS
BEGIN

	DELETE dbo.SecurityXParty
	WHERE	SecurityXPartyId = @SecurityXPartyId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SecurityXPartyId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
