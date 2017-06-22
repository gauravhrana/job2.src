IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderRequestUpdate') 
BEGIN
	DROP Procedure OrderRequestUpdate
END
GO

CREATE Procedure dbo.OrderRequestUpdate
(
		@OrderRequestId				INT
	,	@EventDate				DATETIME
	,	@Notes				VARCHAR(500)
	,	@LastModifiedBy				VARCHAR(500)
	,	@LastModifiedOn				DATETIME
	,	@ParentOrderRequestId				INT
	,	@PortfolioId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderRequest'
)
AS
BEGIN

	UPDATE	dbo.OrderRequest
	SET
			EventDate				=	@EventDate
		,	Notes				=	@Notes
		,	LastModifiedBy				=	@LastModifiedBy
		,	LastModifiedOn				=	@LastModifiedOn
		,	ParentOrderRequestId				=	@ParentOrderRequestId
		,	PortfolioId				=	@PortfolioId
	WHERE	OrderRequestId			=   @OrderRequestId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @OrderRequestId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
