IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MediumOfExchangeUpdate') 
BEGIN
	DROP Procedure MediumOfExchangeUpdate
END
GO

CREATE Procedure dbo.MediumOfExchangeUpdate
(
		@MediumOfExchangeId				INT
	,	@ExtendedDescription				VARCHAR(500)
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'MediumOfExchange'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.MediumOfExchange SET
			ExtendedDescription				=	@ExtendedDescription
		,	Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	MediumOfExchangeId			=   @MediumOfExchangeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MediumOfExchangeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO