IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CaseTypeUpdate') 
BEGIN
	DROP Procedure CaseTypeUpdate
END
GO

CREATE Procedure dbo.CaseTypeUpdate
(
		@CaseTypeId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CaseType'
)
AS
BEGIN

	UPDATE	dbo.CaseType
	SET
			Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
	WHERE	CaseTypeId			=   @CaseTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CaseTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
