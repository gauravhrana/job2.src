IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FundXLegalEntityInsert') 
BEGIN
	DROP Procedure FundXLegalEntityInsert
END
GO

CREATE Procedure dbo.FundXLegalEntityInsert
(
		@FundXLegalEntityId				INT		= NULL 	OUTPUT 
	,	@FundId				INT
	,	@LegalEntityId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'FundXLegalEntity'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@FundXLegalEntityId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.FundXLegalEntity
	(
			FundXLegalEntityId
		,	FundId
		,	LegalEntityId
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@FundXLegalEntityId
		,	@FundId
		,	@LegalEntityId
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @FundXLegalEntityId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @FundXLegalEntityId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
