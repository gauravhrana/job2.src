IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CreditDefaultSwapIndexUpdate') 
	BEGIN
	DROP Procedure CreditDefaultSwapIndexUpdate
END
GO

CREATE Procedure dbo.CreditDefaultSwapIndexUpdate
(
		@CreditDefaultSwapIndexId			INT
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CreditDefaultSwapIndex'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT
	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE dbo.CreditDefaultSwapIndex SET
		Name				=	@Name
	,	Description				=	@Description
	,	SortOrder				=	@SortOrder
	,	UpdatedDate				=	@UpdatedDate
	,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE
		CreditDefaultSwapIndexId			=   @CreditDefaultSwapIndexId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType		= @SystemEntityType
	,	@EntityKey				= @CreditDefaultSwapIndexId
	,	@AuditAction			= 'Update'
	,	@CreatedDate			= @AuditDate
	,	@CreatedByPersonId		= @AuditId

END
GO
