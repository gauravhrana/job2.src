IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationMonitoredEventEmailSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventEmailSearch'
	DROP Procedure ApplicationMonitoredEventEmailSearch
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventEmailSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventEmailSearch
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
			EXEC ApplicationMonitoredEventEmailSearch NULL	, NULL	, NULL
			EXEC ApplicationMonitoredEventEmailSearch NULL	, 'K'	, NULL
			EXEC ApplicationMonitoredEventEmailSearch 1		, 'K'	, NULL
			EXEC ApplicationMonitoredEventEmailSearch 1		, NULL	, NULL
			EXEC ApplicationMonitoredEventEmailSearch NULL	, NULL	, 'W'

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
Create procedure ApplicationMonitoredEventEmailSearch
(
		@ApplicationMonitoredEventEmailId		INT				= NULL 			
	,	@ApplicationMonitoredEventSourceId		INT				= NULL 	
	,	@ApplicationId							INT		
	,	@UserId									INT				= NULL 			
	,	@AuditId								INT								
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationMonitoredEventEmail'
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

	SELECT	a.ApplicationMonitoredEventEmailId 
		,	a.ApplicationMonitoredEventSourceId
		,	a.UserId 
		,	a.CorrespondenceLevel
		,	a.Active
		,	b.Code								AS	'ApplicationMonitoredEventSource'
		,	c.FirstName + c.LastName			AS	'ApplicationUser'	
	INTO		#TempMain				
	FROM		dbo.ApplicationMonitoredEventEmail					a
	INNER JOIN	dbo.ApplicationMonitoredEventSource					b		ON		a.ApplicationMonitoredEventSourceId		= b.ApplicationMonitoredEventSourceId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	c		ON		a.UserId								= c.ApplicationUserId
	WHERE a.ApplicationMonitoredEventSourceId = ISNULL(@ApplicationMonitoredEventSourceId, a.ApplicationMonitoredEventSourceId )
	AND a.UserId		 = ISNULL(@UserId,a.UserId )
	AND a.ApplicationId	 = ISNULL(@ApplicationId, a.ApplicationId )
	AND a.ApplicationMonitoredEventEmailId = ISNULL(@ApplicationMonitoredEventEmailId, a.ApplicationMonitoredEventEmailId )
	
	ORDER BY a.ApplicationMonitoredEventEmailId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ApplicationMonitoredEventEmailId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ApplicationMonitoredEventEmailId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ApplicationMonitoredEventEmailId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ApplicationMonitoredEventEmailId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId	
	
	SELECT 	a.ApplicationMonitoredEventEmailId 
		,	a.ApplicationMonitoredEventSourceId
		,	a.UserId 
		,	a.CorrespondenceLevel
		,	a.Active
		,	a.ApplicationMonitoredEventSource
		,	a.ApplicationUser as 'User'
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.ApplicationMonitoredEventEmailId	= b.ApplicationMonitoredEventEmailId
	ORDER BY	a.ApplicationMonitoredEventEmailId
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
				,	a.ApplicationMonitoredEventEmailId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventEmailId
		,	@SystemEntityType		= @SystemEntityType	
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
		END
END
GO
	

