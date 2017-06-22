IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationRouteSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteSearch'
	DROP Procedure ApplicationRouteSearch
END
GO

PRINT 'Creating Procedure ApplicationRouteSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRouteSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC ApplicationRouteSearch NULL	, NULL	, NULL
			EXEC ApplicationRouteSearch NULL	, 'K'	, NULL
			EXEC ApplicationRouteSearch 1		, 'K'	, NULL
			EXEC ApplicationRouteSearch 1		, NULL	, NULL
			EXEC ApplicationRouteSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure ApplicationRouteSearch
(
		@ApplicationRouteId			INT				= NULL	
	,	@ApplicationId				INT				= NULL	
	,	@RouteName					VARCHAR(100)	= NULL
	,	@EntityName					VARCHAR(100)	= NULL 
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'ApplicationRoute'
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	IF @AddTraceInfo = 1 
	BEGIN		
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'RouteName' 
	SET @InputValuesLocal			= @RouteName  
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.ApplicationRouteSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the ApplicationRoute did not provide any values
	-- assume search on all possiblities ('%')
	SET @RouteName	= ISNULL(@RouteName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@RouteName))) = 0
		BEGIN
			SET	@RouteName = '%'
		END

	-- assume search on all possiblities ('%')
	SET @EntityName	= ISNULL(@EntityName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@EntityName))) = 0
		BEGIN
			SET	@EntityName = '%'
		END
	
	SELECT	a.ApplicationRouteId
		,	a.ApplicationId		
		,	a.RouteName				
		,	a.EntityName			
		,	a.ProposedRoute				
		,	a.RelativeRoute				
		,	a.Description				
	INTO	#TempMain
	FROM	dbo.ApplicationRoute a	
	WHERE	a.RouteName					LIKE @RouteName	+ '%'
	AND		ISNULL(a.EntityName, -1)	LIKE @EntityName
	AND		a.ApplicationId				= ISNULL(@ApplicationId, a.ApplicationId )	
	AND		a.ApplicationRouteId		= ISNULL(@ApplicationRouteId, a.ApplicationRouteId )	
	ORDER BY  a.EntityName			ASC,
			 a.ApplicationRouteId	ASC	
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ApplicationRouteId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN
				 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ApplicationRouteId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ApplicationRouteId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ApplicationRouteId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId		
	
	SELECT 	a.ApplicationRouteId
		,	a.ApplicationId		
		,	a.RouteName				
		,	a.EntityName			
		,	a.ProposedRoute				
		,	a.RelativeRoute				
		,	a.Description			
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.ApplicationRouteId	= b.ApplicationRouteId
	ORDER BY		a.ApplicationRouteId
END
ELSE
	BEGIN
		DECLARE @StaticUpdatedDate AS DATETIME
		SET @StaticUpdatedDate = Convert(datetime, '1/1/1900', 103)
	
		SELECT 	a.*
		   	,	UpdatedDate = @StaticUpdatedDate
			,	UpdatedBy	= 'Unknown'
			,	LastAction	= 'Unknown'
		FROM	#TempMain a		
		ORDER BY	a.ApplicationRouteId
	END
	IF @AddAuditInfo = 1 
	BEGIN

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationRoute'
		,	@EntityKey				= @ApplicationRouteId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId		
	END
END
GO

