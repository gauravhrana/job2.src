IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderStatusInsert') 
BEGIN
	DROP Procedure OrderStatusInsert
END
GO

CREATE Procedure dbo.OrderStatusInsert
(
		@OrderStatusId				INT		= NULL 	OUTPUT 
	,	@OrderId				INT
	,	@Comments				VARCHAR(500)
	,	@LastModifiedBy				VARCHAR(500)
	,	@LastModifiedOn				DATETIME
	,	@OrderStatusTypeId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderStatus'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @OrderStatusId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.OrderStatus
	(
			OrderStatusId
		,	OrderId
		,	Comments
		,	LastModifiedBy
		,	LastModifiedOn
		,	OrderStatusTypeId
		,	ApplicationId
	)
	VALUES
	(
			@OrderStatusId
		,	@OrderId
		,	@Comments
		,	@LastModifiedBy
		,	@LastModifiedOn
		,	@OrderStatusTypeId
		,	@ApplicationId
	)

	SELECT @OrderStatusId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @OrderStatusId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
