IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderStatusTypeInsert') 
BEGIN
	DROP Procedure OrderStatusTypeInsert
END
GO

CREATE Procedure dbo.OrderStatusTypeInsert
(
		@OrderStatusTypeId				INT		= NULL 	OUTPUT 
	,	@Code				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@OrderStatusGroupId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderStatusType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @OrderStatusTypeId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.OrderStatusType
	(
			OrderStatusTypeId
		,	Code
		,	Description
		,	OrderStatusGroupId
		,	ApplicationId
	)
	VALUES
	(
			@OrderStatusTypeId
		,	@Code
		,	@Description
		,	@OrderStatusGroupId
		,	@ApplicationId
	)

	SELECT @OrderStatusTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @OrderStatusTypeId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
