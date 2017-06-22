IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationMonitoredEventSourceSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventSourceSearch'
	DROP Procedure ApplicationMonitoredEventSourceSearch
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventSourceSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventSourceSearch
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
			EXEC ApplicationMonitoredEventSourceSearch NULL	, NULL	, NULL
			EXEC ApplicationMonitoredEventSourceSearch NULL	, 'K'	, NULL
			EXEC ApplicationMonitoredEventSourceSearch 1	, 'K'	, NULL
			EXEC ApplicationMonitoredEventSourceSearch 1	, NULL	, NULL
			EXEC ApplicationMonitoredEventSourceSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
Create procedure ApplicationMonitoredEventSourceSearch
(
		@ApplicationMonitoredEventSourceId		INT				= NULL 			
	,	@ApplicationId							INT				= NULL 		
	,	@Code									VARCHAR(50)		= NULL 			
	,	@AuditId								INT								
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationMonitoredEventSource'
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET @Code	= ISNULL(@Code, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Code))) = 0
		BEGIN
			SET	@Code = '%'
		END

	SELECT	a.*
	INTO	#TempMain
	FROM	dbo.ApplicationMonitoredEventSource a
	WHERE	a.Code LIKE @Code	+ '%'
	AND a.ApplicationId	= ISNULL(@ApplicationId, a.ApplicationId )
	AND a.ApplicationMonitoredEventSourceId	= ISNULL(@ApplicationMonitoredEventSourceId, a.ApplicationMonitoredEventSourceId )
	ORDER BY a.Code		ASC,
			 a.ApplicationMonitoredEventSourceId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ApplicationMonitoredEventSourceId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ApplicationMonitoredEventSourceId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ApplicationMonitoredEventSourceId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ApplicationMonitoredEventSourceId
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
				ON	a.ApplicationMonitoredEventSourceId	= b.ApplicationMonitoredEventSourceId
	ORDER BY	a.ApplicationMonitoredEventSourceId
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
		ORDER BY	a.SortOrder				ASC
				,	a.ApplicationMonitoredEventSourceId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventSourceId
		,	@SystemEntityType		= @SystemEntityType	
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO
	

