IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FundPricesUpdate') 
	BEGIN
	DROP Procedure FundPricesUpdate
END
GO

CREATE Procedure dbo.FundPricesUpdate
(
		@FundPricesId			INT
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'FundPrices'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT
	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE dbo.FundPrices SET
		Name				=	@Name
	,	Description				=	@Description
	,	SortOrder				=	@SortOrder
	,	UpdatedDate				=	@UpdatedDate
	,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE
		FundPricesId			=   @FundPricesId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType		= @SystemEntityType
	,	@EntityKey				= @FundPricesId
	,	@AuditAction			= 'Update'
	,	@CreatedDate			= @AuditDate
	,	@CreatedByPersonId		= @AuditId

END
GO
