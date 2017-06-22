IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderTypeInsert') 
BEGIN
	DROP Procedure OrderTypeInsert
END
GO

CREATE Procedure dbo.OrderTypeInsert
(
		@OrderTypeId				INT		= NULL 	OUTPUT 
	,	@Code				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'OrderType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @OrderTypeId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.OrderType
	(
			OrderTypeId
		,	Code
		,	Description
		,	ApplicationId
	)
	VALUES
	(
			@OrderTypeId
		,	@Code
		,	@Description
		,	@ApplicationId
	)

	SELECT @OrderTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @OrderTypeId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
