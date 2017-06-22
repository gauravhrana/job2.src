IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXInvestmentIdentifierSearch') 
BEGIN
	DROP Procedure SecurityXInvestmentIdentifierSearch
END
GO

CREATE Procedure dbo.SecurityXInvestmentIdentifierSearch
(
		@SecurityXInvestmentIdentifierId				INT		= NULL
	,	@SecurityId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'SecurityXInvestmentIdentifier'
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
			a. SecurityXInvestmentIdentifierId
		,	a. Ticker
		,	a. CUSIP
		,	a. SEDOL
		,	a. ISIN
		,	a. WKN
		,	a. AltID1
		,	a. AltID2
		,	a. AltID3
		,	a. AltID4
		,	a. AltID5
		,	a. SecurityId
		,	Security.Name AS Security
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.SecurityXInvestmentIdentifier a
	INNER JOIN Security ON Security.SecurityId = a.SecurityId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.SecurityXInvestmentIdentifierId = ISNULL(@SecurityXInvestmentIdentifierId, a.SecurityXInvestmentIdentifierId)
	AND		a.SecurityId = ISNULL(@SecurityId, a.SecurityId)
	ORDER BY	a.SecurityXInvestmentIdentifierId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE SecurityXInvestmentIdentifierId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.SecurityXInvestmentIdentifierId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.SecurityXInvestmentIdentifierId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.SecurityXInvestmentIdentifierId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.SecurityXInvestmentIdentifierId=b.SecurityXInvestmentIdentifierId
		ORDER BY	a.SecurityXInvestmentIdentifierId
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
		ORDER BY	a.SecurityXInvestmentIdentifierId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @SecurityXInvestmentIdentifierId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
