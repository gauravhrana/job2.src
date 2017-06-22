IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXExternalMarketDataIdentifierInsert') 
BEGIN
	DROP Procedure SecurityXExternalMarketDataIdentifierInsert
END
GO

CREATE Procedure dbo.SecurityXExternalMarketDataIdentifierInsert
(
		@SecurityXExternalMarketDataIdentifierId				INT		= NULL 	OUTPUT 
	,	@BloombergGlobalId				INT
	,	@BloombergTicker				VARCHAR(500)
	,	@BloombergUniqueId				INT
	,	@BloombergMarketSector				VARCHAR(500)
	,	@RICCode				VARCHAR(500)
	,	@IDCCode				VARCHAR(500)
	,	@RedCode				VARCHAR(500)
	,	@PriceWithSuperDerivatives				VARCHAR(500)
	,	@SecurityId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'SecurityXExternalMarketDataIdentifier'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SecurityXExternalMarketDataIdentifierId Output, @AuditId


	INSERT INTO dbo.SecurityXExternalMarketDataIdentifier
	(
			SecurityXExternalMarketDataIdentifierId
		,	BloombergGlobalId
		,	BloombergTicker
		,	BloombergUniqueId
		,	BloombergMarketSector
		,	RICCode
		,	IDCCode
		,	RedCode
		,	PriceWithSuperDerivatives
		,	SecurityId
		,	ApplicationId
	)
	VALUES
	(
			@SecurityXExternalMarketDataIdentifierId
		,	@BloombergGlobalId
		,	@BloombergTicker
		,	@BloombergUniqueId
		,	@BloombergMarketSector
		,	@RICCode
		,	@IDCCode
		,	@RedCode
		,	@PriceWithSuperDerivatives
		,	@SecurityId
		,	@ApplicationId
	)

	SELECT @SecurityXExternalMarketDataIdentifierId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SecurityXExternalMarketDataIdentifierId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
