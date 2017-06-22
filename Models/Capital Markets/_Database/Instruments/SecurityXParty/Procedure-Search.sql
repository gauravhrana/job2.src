IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXPartySearch') 
BEGIN
	DROP Procedure SecurityXPartySearch
END
GO

CREATE Procedure dbo.SecurityXPartySearch
(
		@SecurityXPartyId				INT		= NULL
	,	@ExchangeId				INT		= NULL
	,	@IssuerId				INT		= NULL
	,	@DeliveryAgentId				INT		= NULL
	,	@SecurityId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'SecurityXParty'
	,	@ApplicationMode				INT							= NULL
	,	@AddAuditInfo					INT							= 1
	,	@AddTraceInfo					INT							= 0
	,	@ReturnAuditInfo				INT							= 0
)
WITH RECOMPILE
AS
BEGIN


	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)


	SELECT 
			a. SecurityXPartyId
		,	a. ExchangeId
		,	Exchange.Name AS Exchange
		,	a. IssuerId
		,	Issuer.Name AS Issuer
		,	a. DeliveryAgentId
		,	DeliveryAgent.Name AS DeliveryAgent
		,	a. SecurityId
		,	Security.Name AS Security
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.SecurityXParty a
	INNER JOIN Exchange ON Exchange.ExchangeId = a.ExchangeId
	INNER JOIN Issuer ON Issuer.IssuerId = a.IssuerId
	INNER JOIN DeliveryAgent ON DeliveryAgent.DeliveryAgentId = a.DeliveryAgentId
	INNER JOIN Security ON Security.SecurityId = a.SecurityId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.SecurityXPartyId = ISNULL(@SecurityXPartyId, a.SecurityXPartyId)
	AND		a.ExchangeId = ISNULL(@ExchangeId, a.ExchangeId)
	AND		a.IssuerId = ISNULL(@IssuerId, a.IssuerId)
	AND		a.DeliveryAgentId = ISNULL(@DeliveryAgentId, a.DeliveryAgentId)
	AND		a.SecurityId = ISNULL(@SecurityId, a.SecurityId)
	ORDER BY	a.SecurityXPartyId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE SecurityXPartyId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.SecurityXPartyId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.SecurityXPartyId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.SecurityXPartyId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.SecurityXPartyId=b.SecurityXPartyId
		ORDER BY	a.SecurityXPartyId
	END
	ELSE
	BEGIN
		DECLARE @StaticUpdatedDate AS DATETIME
		SET @StaticUpdatedDate = Convert(datetime, '1/1/1900', 103)

		SELECT	a.*
			,	UpdatedDate = @StaticUpdatedDate
			,	UpdatedBy	= 'Unknown'
			,	LastAction	= 'Unknown'
		FROM	#TempMain a	
		ORDER BY	a.SecurityXPartyId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @SecurityXPartyId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
