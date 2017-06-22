IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='HolidayXCountrySearch') 
BEGIN
	DROP Procedure HolidayXCountrySearch
END
GO

CREATE Procedure dbo.HolidayXCountrySearch
(
		@HolidayXCountryId				INT		= NULL
	,	@HolidayId				INT		= NULL
	,	@CountryId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'HolidayXCountry'
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
			a. HolidayXCountryId
		,	a. HolidayId
		,	Holiday.Name AS Holiday
		,	a. CountryId
		,	Country.Name AS Country
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.HolidayXCountry a
	INNER JOIN Holiday ON Holiday.HolidayId = a.HolidayId
	INNER JOIN Country ON Country.CountryId = a.CountryId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.HolidayXCountryId = ISNULL(@HolidayXCountryId, a.HolidayXCountryId)
	AND		a.HolidayId = ISNULL(@HolidayId, a.HolidayId)
	AND		a.CountryId = ISNULL(@CountryId, a.CountryId)
	ORDER BY	a.HolidayXCountryId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE HolidayXCountryId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.HolidayXCountryId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.HolidayXCountryId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.HolidayXCountryId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.HolidayXCountryId=b.HolidayXCountryId
		ORDER BY	a.HolidayXCountryId
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
		ORDER BY	a.HolidayXCountryId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @HolidayXCountryId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
