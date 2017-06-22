IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ReportingRequirementInsert') 
BEGIN
	DROP Procedure ReportingRequirementInsert
END
GO

CREATE Procedure dbo.ReportingRequirementInsert
(
		@ReportingRequirementId				INT		= NULL 	OUTPUT 
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'ReportingRequirement'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@ReportingRequirementId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.ReportingRequirement
	(
			ReportingRequirementId
		,	Name
		,	Description
		,	SortOrder
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@ReportingRequirementId
		,	@Name
		,	@Description
		,	@SortOrder
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @ReportingRequirementId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ReportingRequirementId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
