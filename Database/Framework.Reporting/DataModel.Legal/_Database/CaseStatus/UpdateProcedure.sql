IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CaseStatusUpdate') 
BEGIN
	DROP Procedure CaseStatusUpdate
END
GO

CREATE Procedure dbo.CaseStatusUpdate
(
		@CaseStatusId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CaseStatus'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.CaseStatus SET
			Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	CaseStatusId			=   @CaseStatusId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CaseStatusId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
