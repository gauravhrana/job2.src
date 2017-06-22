IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderTypeUpdate') 
BEGIN
	DROP Procedure OrderTypeUpdate
END
GO

CREATE Procedure dbo.OrderTypeUpdate
(
		@OrderTypeId				INT
	,	@Code				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderType'
)
AS
BEGIN

	UPDATE	dbo.OrderType
	SET
			Code				=	@Code
		,	Description				=	@Description
	WHERE	OrderTypeId			=   @OrderTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @OrderTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
