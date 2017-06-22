IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FundXLegalEntityDelete') 
BEGIN
	DROP Procedure FundXLegalEntityDelete
END
GO

CREATE Procedure dbo.FundXLegalEntityDelete
(
		@FundXLegalEntityId				INT		= NULL
	,	@FundId				INT		= NULL
	,	@LegalEntityId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'FundXLegalEntity'
)
AS
BEGIN

	DELETE dbo.FundXLegalEntity
	WHERE		FundXLegalEntityId = ISNULL(@FundXLegalEntityId, FundXLegalEntityId)
	AND			FundId = ISNULL(@FundId, FundId)
	AND			LegalEntityId = ISNULL(@LegalEntityId, LegalEntityId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @FundXLegalEntityId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
