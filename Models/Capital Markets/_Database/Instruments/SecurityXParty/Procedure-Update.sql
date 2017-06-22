IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXPartyUpdate') 
BEGIN
	DROP Procedure SecurityXPartyUpdate
END
GO

CREATE Procedure dbo.SecurityXPartyUpdate
(
		@SecurityXPartyId				INT
	,	@ExchangeId				INT
	,	@IssuerId				INT
	,	@DeliveryAgentId				INT
	,	@SecurityId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'SecurityXParty'
)
AS
BEGIN

	UPDATE	dbo.SecurityXParty
	SET
			ExchangeId				=	@ExchangeId
		,	IssuerId				=	@IssuerId
		,	DeliveryAgentId				=	@DeliveryAgentId
		,	SecurityId				=	@SecurityId
	WHERE	SecurityXPartyId			=   @SecurityXPartyId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SecurityXPartyId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
