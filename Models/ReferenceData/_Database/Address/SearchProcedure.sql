IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AddressSearch') 
BEGIN
	DROP Procedure AddressSearch
END
GO

CREATE Procedure dbo.AddressSearch
(
		@AddressId				INT		= NULL
	,	@CityId				INT		= NULL
	,	@StateId				INT		= NULL
	,	@CountryId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'Address'
	,	@ApplicationMode				INT							= NULL
	,	@AddAuditInfo					INT							= 1
	,	@AddTraceInfo					INT							= 0
	,	@ReturnAuditInfo				INT							= 0
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON

	IF @AddTraceInfo = 1 
	BEGIN

		-- TRACE --
		DECLARE @InputParametersLocal	VARCHAR(500)  
		DECLARE @InputValuesLocal		VARCHAR(5000)  
		SET @InputParametersLocal		=  'AddressId' 
		SET @InputValuesLocal			=  CAST(@AddressId AS VARCHAR(50))

		EXEC dbo.StoredProcedureLogInsert
				@Name					= 'dbo.AddressSearch'
			,	@InputParameters		= @InputParametersLocal
			,	@InputValues			= @InputValuesLocal	
			-- TRACE --		

	END	

	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)


	SELECT 
			a. AddressId
		,	a. CityId
		,	City.Name AS City
		,	a. StateId
		,	State.Name AS State
		,	a. CountryId
		,	Country.Name AS Country
		,	a. Address1
		,	a. Address2
		,	a. PostalCode
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.Address a
	INNER JOIN City ON City.CityId = a.CityId
	INNER JOIN State ON State.StateId = a.StateId
	INNER JOIN Country ON Country.CountryId = a.CountryId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId	, a.ApplicationId)	
	AND		a.AddressId =
			CASE
				WHEN @AddressId IS NULL THEN a.AddressId
				ELSE @AddressId
			END
	AND		a.CityId =
			CASE
				WHEN @CityId IS NULL THEN a.CityId
				ELSE @CityId
			END
	AND		a.StateId =
			CASE
				WHEN @StateId IS NULL THEN a.StateId
				ELSE @StateId
			END
	AND		a.CountryId =
			CASE
				WHEN @CountryId IS NULL THEN a.CountryId
				ELSE @CountryId
			END
	ORDER BY	a.AddressId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE AddressId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.AddressId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.AddressId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.AddressId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.AddressId=b.AddressId
		ORDER BY	a.AddressId
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
		ORDER BY	a.AddressId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @AddressId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
