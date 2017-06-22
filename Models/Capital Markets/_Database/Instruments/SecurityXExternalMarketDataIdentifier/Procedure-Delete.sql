IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXExternalMarketDataIdentifierDelete') 
BEGIN
	DROP Procedure SecurityXExternalMarketDataIdentifierDelete
END
GO

CREATE Procedure dbo.SecurityXExternalMarketDataIdentifierDelete
(
		@SecurityXExternalMarketDataIdentifierId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'SecurityXExternalMarketDataIdentifier'
)
AS
BEGIN

	DELETE dbo.SecurityXExternalMarketDataIdentifier
	WHERE	SecurityXExternalMarketDataIdentifierId = @SecurityXExternalMarketDataIdentifierId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SecurityXExternalMarketDataIdentifierId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
