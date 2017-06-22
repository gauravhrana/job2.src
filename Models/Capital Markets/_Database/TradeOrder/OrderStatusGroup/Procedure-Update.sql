IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderStatusGroupUpdate') 
BEGIN
	DROP Procedure OrderStatusGroupUpdate
END
GO

CREATE Procedure dbo.OrderStatusGroupUpdate
(
		@OrderStatusGroupId				INT
	,	@OrderStatusGroupCode				VARCHAR(500)
	,	@OrderStatusGroupDescription				VARCHAR(500)
	,	@OrderProcessFlag				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderStatusGroup'
)
AS
BEGIN

	UPDATE	dbo.OrderStatusGroup
	SET
			OrderStatusGroupCode				=	@OrderStatusGroupCode
		,	OrderStatusGroupDescription				=	@OrderStatusGroupDescription
		,	OrderProcessFlag				=	@OrderProcessFlag
	WHERE	OrderStatusGroupId			=   @OrderStatusGroupId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @OrderStatusGroupId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
