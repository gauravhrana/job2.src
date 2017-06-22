IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderStatusUpdate') 
BEGIN
	DROP Procedure OrderStatusUpdate
END
GO

CREATE Procedure dbo.OrderStatusUpdate
(
		@OrderStatusId				INT
	,	@OrderId				INT
	,	@Comments				VARCHAR(500)
	,	@LastModifiedBy				VARCHAR(500)
	,	@LastModifiedOn				DATETIME
	,	@OrderStatusTypeId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderStatus'
)
AS
BEGIN

	UPDATE	dbo.OrderStatus
	SET
			OrderId				=	@OrderId
		,	Comments				=	@Comments
		,	LastModifiedBy				=	@LastModifiedBy
		,	LastModifiedOn				=	@LastModifiedOn
		,	OrderStatusTypeId				=	@OrderStatusTypeId
	WHERE	OrderStatusId			=   @OrderStatusId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @OrderStatusId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
