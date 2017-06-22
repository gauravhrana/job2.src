IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FinancialAccountTypeUpdate') 
BEGIN
	DROP Procedure FinancialAccountTypeUpdate
END
GO

CREATE Procedure dbo.FinancialAccountTypeUpdate
(
		@FinancialAccountTypeId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'FinancialAccountType'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.FinancialAccountType SET
			Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	FinancialAccountTypeId			=   @FinancialAccountTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FinancialAccountTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
