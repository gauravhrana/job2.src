IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='EventSubTypeUpdate') 
BEGIN
	DROP Procedure EventSubTypeUpdate
END
GO

CREATE Procedure dbo.EventSubTypeUpdate
(
		@EventSubTypeId				INT
	,	@EventTypeId				INT
	,	@PersonId				INT
	,	@EventKey				VARCHAR(500)
	,	@SortOrder				INT
	,	@Value				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'EventSubType'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.EventSubType SET
			EventTypeId				=	@EventTypeId
		,	PersonId				=	@PersonId
		,	EventKey				=	@EventKey
		,	SortOrder				=	@SortOrder
		,	Value				=	@Value
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	EventSubTypeId			=   @EventSubTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @EventSubTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
