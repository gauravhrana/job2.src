IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PriceScheduleXPriceListSearch') 
BEGIN
	DROP Procedure PriceScheduleXPriceListSearch
END
GO

CREATE Procedure dbo.PriceScheduleXPriceListSearch
(
		@PriceScheduleXPriceListId				INT		= NULL
	,	@PriceScheduleId				INT		= NULL
	,	@PriceListId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'PriceScheduleXPriceList'
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
		SET @InputParametersLocal		=  'PriceScheduleXPriceListId' 
		SET @InputValuesLocal			=  CAST(@PriceScheduleXPriceListId AS VARCHAR(50))

		EXEC dbo.StoredProcedureLogInsert
				@Name					= 'dbo.PriceScheduleXPriceListSearch'
			,	@InputParameters		= @InputParametersLocal
			,	@InputValues			= @InputValuesLocal	
			-- TRACE --		

	END	

	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)


	SELECT 
			a. PriceScheduleXPriceListId
		,	a. PriceScheduleId
		,	a. PriceListId
		,	PriceSchedule.Name AS PriceSchedule
		,	PriceList.Name AS PriceList
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.PriceScheduleXPriceList a
	INNER JOIN PriceSchedule ON PriceSchedule.PriceScheduleId = a.PriceScheduleId
	INNER JOIN PriceList ON PriceList.PriceListId = a.PriceListId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId	, a.ApplicationId)	
	AND		a.PriceScheduleXPriceListId =
			CASE
				WHEN @PriceScheduleXPriceListId IS NULL THEN a.PriceScheduleXPriceListId
				ELSE @PriceScheduleXPriceListId
			END
	AND		a.PriceScheduleId =
			CASE
				WHEN @PriceScheduleId IS NULL THEN a.PriceScheduleId
				ELSE @PriceScheduleId
			END
	AND		a.PriceListId =
			CASE
				WHEN @PriceListId IS NULL THEN a.PriceListId
				ELSE @PriceListId
			END
	ORDER BY	a.PriceScheduleXPriceListId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE PriceScheduleXPriceListId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.PriceScheduleXPriceListId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.PriceScheduleXPriceListId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.PriceScheduleXPriceListId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.PriceScheduleXPriceListId=b.PriceScheduleXPriceListId
		ORDER BY	a.PriceScheduleXPriceListId
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
		ORDER BY	a.PriceScheduleXPriceListId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @PriceScheduleXPriceListId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
