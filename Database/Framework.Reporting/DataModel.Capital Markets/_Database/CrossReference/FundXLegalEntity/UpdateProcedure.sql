IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FundXLegalEntityUpdate') 
BEGIN
	DROP Procedure FundXLegalEntityUpdate
END
GO

CREATE Procedure dbo.FundXLegalEntityUpdate
(
		@FundXLegalEntityId				INT
	,	@FundId				INT
	,	@LegalEntityId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'FundXLegalEntity'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.FundXLegalEntity SET
			FundId				=	@FundId
		,	LegalEntityId				=	@LegalEntityId
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	FundXLegalEntityId			=   @FundXLegalEntityId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FundXLegalEntityId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
