IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXExternalMarketDataIdentifierSearch') 
BEGIN
	DROP Procedure SecurityXExternalMarketDataIdentifierSearch
END
GO

CREATE Procedure dbo.SecurityXExternalMarketDataIdentifierSearch
(
		@SecurityXExternalMarketDataIdentifierId				INT		= NULL
	,	@SecurityId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'SecurityXExternalMarketDataIdentifier'
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
			a. SecurityXExternalMarketDataIdentifierId
		,	a. BloombergGlobalId
		,	a. BloombergTicker
		,	a. BloombergUniqueId
		,	a. BloombergMarketSector
		,	a. RICCode
		,	a. IDCCode
		,	a. RedCode
		,	a. PriceWithSuperDerivatives
		,	a. SecurityId
		,	Security.Name AS Security
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.SecurityXExternalMarketDataIdentifier a
	INNER JOIN Security ON Security.SecurityId = a.SecurityId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.SecurityXExternalMarketDataIdentifierId = ISNULL(@SecurityXExternalMarketDataIdentifierId, a.SecurityXExternalMarketDataIdentifierId)
	AND		a.SecurityId = ISNULL(@SecurityId, a.SecurityId)
	ORDER BY	a.SecurityXExternalMarketDataIdentifierId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE SecurityXExternalMarketDataIdentifierId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.SecurityXExternalMarketDataIdentifierId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.SecurityXExternalMarketDataIdentifierId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.SecurityXExternalMarketDataIdentifierId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.SecurityXExternalMarketDataIdentifierId=b.SecurityXExternalMarketDataIdentifierId
		ORDER BY	a.SecurityXExternalMarketDataIdentifierId
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
		ORDER BY	a.SecurityXExternalMarketDataIdentifierId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @SecurityXExternalMarketDataIdentifierId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
