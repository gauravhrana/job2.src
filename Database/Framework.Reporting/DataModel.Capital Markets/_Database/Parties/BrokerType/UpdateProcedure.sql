IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='BrokerTypeUpdate') 
BEGIN
	DROP Procedure BrokerTypeUpdate
END
GO

CREATE Procedure dbo.BrokerTypeUpdate
(
		@BrokerTypeId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'BrokerType'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.BrokerType SET
			Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	BrokerTypeId			=   @BrokerTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BrokerTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
