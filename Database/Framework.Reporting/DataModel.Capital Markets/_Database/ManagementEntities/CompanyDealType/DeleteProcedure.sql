IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CompanyDealTypeDelete') 
BEGIN
	DROP Procedure CompanyDealTypeDelete
END
GO

CREATE Procedure dbo.CompanyDealTypeDelete
(
		@CompanyDealTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'CompanyDealType'
)
AS
BEGIN

	DELETE dbo.CompanyDealType
	WHERE	CompanyDealTypeId = @CompanyDealTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CompanyDealTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
