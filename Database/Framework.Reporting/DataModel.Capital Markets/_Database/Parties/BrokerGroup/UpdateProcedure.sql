IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='BrokerGroupUpdate') 
BEGIN
	DROP Procedure BrokerGroupUpdate
END
GO

CREATE Procedure dbo.BrokerGroupUpdate
(
		@BrokerGroupId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'BrokerGroup'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.BrokerGroup SET
			Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	BrokerGroupId			=   @BrokerGroupId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BrokerGroupId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
