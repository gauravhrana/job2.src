IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ProvinceSearch') 
BEGIN
	DROP Procedure ProvinceSearch
END
GO

CREATE Procedure dbo.ProvinceSearch
(
		@ProvinceId				INT		= NULL
	,	@CountryId				INT		= NULL
	,	@ProvinceTypeId				INT		= NULL
	,	@Name				VARCHAR(500)		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'Province'
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
		SET @InputParametersLocal		=  'ProvinceId' 
		SET @InputValuesLocal			=  CAST(@ProvinceId AS VARCHAR(50))

		EXEC dbo.StoredProcedureLogInsert
				@Name					= 'dbo.ProvinceSearch'
			,	@InputParameters		= @InputParametersLocal
			,	@InputValues			= @InputValuesLocal	
			-- TRACE --		

	END	

	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	--if blank, then assume search on all possiblities ('%')
	IF  @Name  IS NULL OR LEN(RTRIM(LTRIM(@Name))) = 0
	BEGIN
		SET	@Name = '%'
	END

	SELECT 
			a. ProvinceId
		,	a. CountryId
		,	Country.Name AS Country
		,	a. ProvinceTypeId
		,	ProvinceType.Name AS ProvinceType
		,	a. Name
		,	a. Description
		,	a. SortOrder
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.Province a
	INNER JOIN Country ON Country.CountryId = a.CountryId
	INNER JOIN ProvinceType ON ProvinceType.ProvinceTypeId = a.ProvinceTypeId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId	, a.ApplicationId)	
	AND		a.ProvinceId =
			CASE
				WHEN @ProvinceId IS NULL THEN a.ProvinceId
				ELSE @ProvinceId
			END
	AND		a.CountryId =
			CASE
				WHEN @CountryId IS NULL THEN a.CountryId
				ELSE @CountryId
			END
	AND		a.ProvinceTypeId =
			CASE
				WHEN @ProvinceTypeId IS NULL THEN a.ProvinceTypeId
				ELSE @ProvinceTypeId
			END
	AND		a.Name	LIKE	@Name + '%'
	ORDER BY	a.ProvinceId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE ProvinceId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.ProvinceId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.ProvinceId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.ProvinceId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.ProvinceId=b.ProvinceId
		ORDER BY	a.ProvinceId
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
		ORDER BY	a.ProvinceId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @ProvinceId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
