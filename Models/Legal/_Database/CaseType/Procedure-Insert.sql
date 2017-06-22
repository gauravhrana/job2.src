IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CaseTypeInsert') 
BEGIN
	DROP Procedure CaseTypeInsert
END
GO

CREATE Procedure dbo.CaseTypeInsert
(
		@CaseTypeId				INT		= NULL 	OUTPUT 
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CaseType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CaseTypeId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.CaseType
	(
			CaseTypeId
		,	Name
		,	Description
		,	SortOrder
		,	ApplicationId
	)
	VALUES
	(
			@CaseTypeId
		,	@Name
		,	@Description
		,	@SortOrder
		,	@ApplicationId
	)

	SELECT @CaseTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CaseTypeId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
