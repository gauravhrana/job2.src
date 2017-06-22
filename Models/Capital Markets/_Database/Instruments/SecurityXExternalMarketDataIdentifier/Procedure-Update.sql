IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXExternalMarketDataIdentifierUpdate') 
BEGIN
	DROP Procedure SecurityXExternalMarketDataIdentifierUpdate
END
GO

CREATE Procedure dbo.SecurityXExternalMarketDataIdentifierUpdate
(
		@SecurityXExternalMarketDataIdentifierId				INT
	,	@BloombergGlobalId				INT
	,	@BloombergTicker				VARCHAR(500)
	,	@BloombergUniqueId				INT
	,	@BloombergMarketSector				VARCHAR(500)
	,	@RICCode				VARCHAR(500)
	,	@IDCCode				VARCHAR(500)
	,	@RedCode				VARCHAR(500)
	,	@PriceWithSuperDerivatives				VARCHAR(500)
	,	@SecurityId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'SecurityXExternalMarketDataIdentifier'
)
AS
BEGIN
			BloombergGlobalId				=	@BloombergGlobalId
		,	BloombergTicker				=	@BloombergTicker
		,	BloombergUniqueId				=	@BloombergUniqueId
		,	BloombergMarketSector				=	@BloombergMarketSector
		,	RICCode				=	@RICCode
		,	IDCCode				=	@IDCCode
		,	RedCode				=	@RedCode
		,	PriceWithSuperDerivatives				=	@PriceWithSuperDerivatives
		,	SecurityId				=	@SecurityId
	WHERE	SecurityXExternalMarketDataIdentifierId			=   @SecurityXExternalMarketDataIdentifierId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SecurityXExternalMarketDataIdentifierId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
