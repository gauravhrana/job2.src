IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='InvestingFeederUpdate') 
	BEGIN
	DROP Procedure InvestingFeederUpdate
END
GO

CREATE Procedure dbo.InvestingFeederUpdate
(
		@InvestingFeederId			INT
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'InvestingFeeder'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT
	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE dbo.InvestingFeeder SET
		Name				=	@Name
	,	Description				=	@Description
	,	SortOrder				=	@SortOrder
	,	UpdatedDate				=	@UpdatedDate
	,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE
		InvestingFeederId			=   @InvestingFeederId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType		= @SystemEntityType
	,	@EntityKey				= @InvestingFeederId
	,	@AuditAction			= 'Update'
	,	@CreatedDate			= @AuditDate
	,	@CreatedByPersonId		= @AuditId

END
GO
