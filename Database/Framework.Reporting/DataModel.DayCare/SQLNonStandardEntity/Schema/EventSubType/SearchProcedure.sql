IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='EventSubTypeSearch') 
BEGIN
	DROP Procedure EventSubTypeSearch
END
GO

CREATE Procedure dbo.EventSubTypeSearch
(
		@EventSubTypeId				INT		= NULL
	,	@EventTypeId				INT		= NULL
	,	@PersonId				INT		= NULL
	,	@EventKey				VARCHAR(500)		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'EventSubType'
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
		SET @InputParametersLocal		=  'EventSubTypeId' 
		SET @InputValuesLocal			=  CAST(@EventSubTypeId AS VARCHAR(50))

		EXEC dbo.StoredProcedureLogInsert
				@Name					= 'dbo.EventSubTypeSearch'
			,	@InputParameters		= @InputParametersLocal
			,	@InputValues			= @InputValuesLocal	
			-- TRACE --		

	END	

	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	--if blank, then assume search on all possiblities ('%')
	IF  @EventKey  IS NULL OR LEN(RTRIM(LTRIM(@EventKey))) = 0
	BEGIN
		SET	@EventKey = '%'
	END

	SELECT 
			a. EventSubTypeId
		,	a. EventTypeId
		,	EventType.Name AS EventType
		,	a. PersonId
		,	AuthenticationAndAuthorization.dbo.ApplicationUser.ApplicationUserName AS Person
		,	a. EventKey
		,	a. SortOrder
		,	a. Value
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.EventSubType a
	INNER JOIN EventType ON EventType.EventTypeId = a.EventTypeId
	INNER JOIN AuthenticationAndAuthorization.dbo.ApplicationUser ON AuthenticationAndAuthorization.dbo.ApplicationUser.ApplicationUserId = a.PersonId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId	, a.ApplicationId)	
	AND		a.EventSubTypeId =
			CASE
				WHEN @EventSubTypeId IS NULL THEN a.EventSubTypeId
				ELSE @EventSubTypeId
			END
	AND		a.EventTypeId =
			CASE
				WHEN @EventTypeId IS NULL THEN a.EventTypeId
				ELSE @EventTypeId
			END
	AND		a.PersonId =
			CASE
				WHEN @PersonId IS NULL THEN a.PersonId
				ELSE @PersonId
			END
	AND		a.EventKey	LIKE	@EventKey + '%'
	ORDER BY	a.EventSubTypeId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE EventSubTypeId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.EventSubTypeId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.EventSubTypeId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.EventSubTypeId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.EventSubTypeId=b.EventSubTypeId
		ORDER BY	a.EventSubTypeId
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
		ORDER BY	a.EventSubTypeId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @EventSubTypeId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
