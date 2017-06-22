IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXPartyInsert') 
BEGIN
	DROP Procedure SecurityXPartyInsert
END
GO

CREATE Procedure dbo.SecurityXPartyInsert
(
		@SecurityXPartyId				INT		= NULL 	OUTPUT 
	,	@ExchangeId				INT
	,	@IssuerId				INT
	,	@DeliveryAgentId				INT
	,	@SecurityId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'SecurityXParty'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SecurityXPartyId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.SecurityXParty
	(
			SecurityXPartyId
		,	ExchangeId
		,	IssuerId
		,	DeliveryAgentId
		,	SecurityId
		,	ApplicationId
	)
	VALUES
	(
			@SecurityXPartyId
		,	@ExchangeId
		,	@IssuerId
		,	@DeliveryAgentId
		,	@SecurityId
		,	@ApplicationId
	)

	SELECT @SecurityXPartyId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SecurityXPartyId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
