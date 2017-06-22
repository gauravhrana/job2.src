IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MSPAFileEventTypeUpdate') 
BEGIN
	DROP Procedure MSPAFileEventTypeUpdate
END
GO

CREATE Procedure dbo.MSPAFileEventTypeUpdate
(
		@MSPAFileEventTypeId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'MSPAFileEventType'
)
AS
BEGIN

	UPDATE	dbo.MSPAFileEventType
	SET
			Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
	WHERE	MSPAFileEventTypeId			=   @MSPAFileEventTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MSPAFileEventTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
