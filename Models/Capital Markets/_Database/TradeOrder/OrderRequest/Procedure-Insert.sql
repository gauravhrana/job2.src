IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderRequestInsert') 
BEGIN
	DROP Procedure OrderRequestInsert
END
GO

CREATE Procedure dbo.OrderRequestInsert
(
		@OrderRequestId				INT		= NULL 	OUTPUT 
	,	@EventDate				DATETIME
	,	@Notes				VARCHAR(500)
	,	@LastModifiedBy				VARCHAR(500)
	,	@LastModifiedOn				DATETIME
	,	@ParentOrderRequestId				INT
	,	@PortfolioId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderRequest'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @OrderRequestId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.OrderRequest
	(
			OrderRequestId
		,	EventDate
		,	Notes
		,	LastModifiedBy
		,	LastModifiedOn
		,	ParentOrderRequestId
		,	PortfolioId
		,	ApplicationId
	)
	VALUES
	(
			@OrderRequestId
		,	@EventDate
		,	@Notes
		,	@LastModifiedBy
		,	@LastModifiedOn
		,	@ParentOrderRequestId
		,	@PortfolioId
		,	@ApplicationId
	)

	SELECT @OrderRequestId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @OrderRequestId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
