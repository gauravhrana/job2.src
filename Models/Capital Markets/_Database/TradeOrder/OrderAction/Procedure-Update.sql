IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderActionUpdate') 
BEGIN
	DROP Procedure OrderActionUpdate
END
GO

CREATE Procedure dbo.OrderActionUpdate
(
		@OrderActionId				INT
	,	@OrderActionCode				VARCHAR(500)
	,	@OrderActionDescription				VARCHAR(500)
	,	@PositionDirection				VARCHAR(500)
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderAction'
)
AS
BEGIN

	UPDATE	dbo.OrderAction
	SET
			OrderActionCode				=	@OrderActionCode
		,	OrderActionDescription				=	@OrderActionDescription
		,	PositionDirection				=	@PositionDirection
	WHERE	OrderActionId			=   @OrderActionId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @OrderActionId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
