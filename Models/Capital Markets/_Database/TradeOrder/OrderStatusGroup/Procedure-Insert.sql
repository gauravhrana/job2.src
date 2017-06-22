IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderStatusGroupInsert') 
BEGIN
	DROP Procedure OrderStatusGroupInsert
END
GO

CREATE Procedure dbo.OrderStatusGroupInsert
(
		@OrderStatusGroupId				INT		= NULL 	OUTPUT 
	,	@OrderStatusGroupCode				VARCHAR(500)
	,	@OrderStatusGroupDescription				VARCHAR(500)
	,	@OrderProcessFlag				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderStatusGroup'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @OrderStatusGroupId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.OrderStatusGroup
	(
			OrderStatusGroupId
		,	OrderStatusGroupCode
		,	OrderStatusGroupDescription
		,	OrderProcessFlag
		,	ApplicationId
	)
	VALUES
	(
			@OrderStatusGroupId
		,	@OrderStatusGroupCode
		,	@OrderStatusGroupDescription
		,	@OrderProcessFlag
		,	@ApplicationId
	)

	SELECT @OrderStatusGroupId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @OrderStatusGroupId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
