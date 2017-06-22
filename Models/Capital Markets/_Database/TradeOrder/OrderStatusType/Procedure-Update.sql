IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderStatusTypeUpdate') 
BEGIN
	DROP Procedure OrderStatusTypeUpdate
END
GO

CREATE Procedure dbo.OrderStatusTypeUpdate
(
		@OrderStatusTypeId				INT
	,	@Code				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@OrderStatusGroupId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderStatusType'
)
AS
BEGIN

	UPDATE	dbo.OrderStatusType
	SET
			Code				=	@Code
		,	Description				=	@Description
		,	OrderStatusGroupId				=	@OrderStatusGroupId
	WHERE	OrderStatusTypeId			=   @OrderStatusTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @OrderStatusTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
