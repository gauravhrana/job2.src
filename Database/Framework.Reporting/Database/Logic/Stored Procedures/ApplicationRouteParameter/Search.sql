IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationRouteParameterSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteParameterSearch'
	DROP Procedure ApplicationRouteParameterSearch
END
GO

PRINT 'Creating Procedure ApplicationRouteParameterSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRouteParameterSearch
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
			EXEC ApplicationRouteParameterSearch NULL	, NULL	, NULL
			EXEC ApplicationRouteParameterSearch NULL	, 'K'	, NULL
			EXEC ApplicationRouteParameterSearch 1		, 'K'	, NULL
			EXEC ApplicationRouteParameterSearch 1		, NULL	, NULL
			EXEC ApplicationRouteParameterSearch NULL	, NULL	, 'W'

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
Create procedure ApplicationRouteParameterSearch
(
		@ApplicationRouteParameterId	INT					= NULL	
	,	@ApplicationId					INT					= NULL	
	,	@ApplicationRouteId				INT					= NULL	
	,	@ParameterName					VARCHAR(100)		= NULL 			
	,	@AuditId						INT								
	,	@AuditDate						DATETIME			= NULL			
	,	@SystemEntityType				VARCHAR(50)			= 'ApplicationRouteParameter'
	,	@ApplicationMode				INT					= NULL		
	,	@AddAuditInfo					INT					= 1
	,	@AddTraceInfo					INT					= 0
	,	@ReturnAuditInfo				INT					= 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	IF @AddTraceInfo = 1 
		BEGIN
	
			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'ParameterName' 
			SET @InputValuesLocal			= @ParameterName  
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.ApplicationRouteParameterSearch'
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal	
				--,	@ExecutedBy					= 'System'	
	
		END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the ApplicationRouteParameter did not provide any values
	-- assume search on all possiblities ('%')
	SET @ParameterName	= ISNULL(@ParameterName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@ParameterName))) = 0
		BEGIN
			SET	@ParameterName = '%'
		END
	
	SELECT	a.ApplicationRouteParameterId	
		,	a.ApplicationRouteId
		,	a.ApplicationId			
		,	a.ParameterName		
		,	a.ParameterValue
		,	b.RouteName	AS 'RouteName'	
		,	b.RouteName	AS 'ApplicationRoute'	
	INTO	#TempMain
	FROM	dbo.ApplicationRouteParameter a	 
	INNER JOIN dbo.ApplicationRoute b on a.ApplicationRouteId=b.ApplicationRouteId	 
	WHERE	a.ParameterName LIKE @ParameterName	+ '%'
	AND		b.ApplicationRouteId			= ISNULL(@ApplicationRouteId, b.ApplicationRouteId )
	AND		a.ApplicationId					= ISNULL(@ApplicationId, a.ApplicationId )
	AND		a.ApplicationRouteParameterId	= ISNULL(@ApplicationRouteParameterId, a.ApplicationRouteParameterId )		
	ORDER BY  a.ParameterName				ASC,
			 a.ApplicationRouteParameterId	ASC	
	
	IF	@ApplicationMode = 1 
		BEGIN		
			DELETE FROM #TempMain
			WHERE ApplicationRouteParameterId < 0
		END
			
	IF @ReturnAuditInfo = 1
		BEGIN		 
		
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.ApplicationRouteParameterId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.ApplicationRouteParameterId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.ApplicationRouteParameterId
			INNER JOIN	CommonServices.dbo.AuditHistory						c
						ON	c.AuditHistoryId	= b.MaxAuditHistoryId
			INNER JOIN	CommonServices.dbo.AuditAction						d	
						ON	c.AuditActionId 	= d.AuditActionId
			INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
						ON	c.CreatedByPersonId	= e.ApplicationUserId		
	
			SELECT 	a.*			
				, 	b.UpdatedDate
				,	b.UpdatedBy
				,	b.LastAction
			FROM #TempMain a
			LEFT JOIN #HistortyInfoDetails	b	
						ON	a.ApplicationRouteParameterId	= b.ApplicationRouteParameterId
			ORDER BY		a.ApplicationRouteParameterId

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
			ORDER BY	a.ApplicationRouteParameterId

		END
	
	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'ApplicationRouteParameter'
				,	@EntityKey				= @ApplicationRouteParameterId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId	

		END
	
END
GO
	

