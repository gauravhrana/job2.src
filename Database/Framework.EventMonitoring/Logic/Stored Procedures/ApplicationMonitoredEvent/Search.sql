IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationMonitoredEventSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventSearch'
	DROP Procedure ApplicationMonitoredEventSearch
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventSearch
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
			EXEC ApplicationMonitoredEventSearch NULL	, NULL	, NULL
			EXEC ApplicationMonitoredEventSearch NULL	, 'K'	, NULL
			EXEC ApplicationMonitoredEventSearch 1		, 'K'	, NULL
			EXEC ApplicationMonitoredEventSearch 1		, NULL	, NULL
			EXEC ApplicationMonitoredEventSearch NULL	, NULL	, 'W'

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
Create procedure ApplicationMonitoredEventSearch
(
		@ApplicationMonitoredEventId				INT				= NULL 			 			
	,	@ApplicationMonitoredEventSourceId			INT				= NULL 			
	,	@ApplicationMonitoredEventProcessingStateId	INT				= NULL 		
	,	@ApplicationId								INT	
	,	@ReferenceId								INT									
	,	@Category									VARCHAR(50)						
	,	@LastModifiedBy								VARCHAR(50)						
	,	@AuditId									INT								
	,	@AuditDate									DATETIME		= NULL 
	,	@SystemEntityType							VARCHAR(50)		= 'ApplicationMonitoredEvent'
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

	SET @Category	= ISNULL(@Category, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Category))) = 0
		BEGIN
			SET	@Category = '%'
		END

	SET @LastModifiedBy	= ISNULL(@LastModifiedBy, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@LastModifiedBy))) = 0
		BEGIN
			SET	@LastModifiedBy = '%'
		END

	SELECT	a.ApplicationMonitoredEventId
		,	a.ApplicationMonitoredEventSourceId			
		,	a.ApplicationMonitoredEventProcessingStateId	
		,	a.ReferenceId	
		,	a.ApplicationId								
		,	a.ReferenceCode								
		,	a.Category	
		,	a.Message 		
		,	a.IsDuplicate		
		,	a.LastModifiedBy	
		,	a.LastModifiedOn											
		,	b.Code								AS	'ApplicationMonitoredEventSource'
		,	c.Code								AS	'ApplicationMonitoredEventProcessingState'	
	INTO		#TempMain				
	FROM		dbo.ApplicationMonitoredEvent					a
	INNER JOIN	dbo.ApplicationMonitoredEventSource				b		ON		a.ApplicationMonitoredEventSourceId				= b.ApplicationMonitoredEventSourceId
	INNER JOIN	dbo.ApplicationMonitoredEventProcessingState	c		ON		a.ApplicationMonitoredEventProcessingStateId	= c.ApplicationMonitoredEventProcessingStateId
	WHERE	a.ReferenceId	=	ISNULL	(@ReferenceId, a.ReferenceId)
	AND		a.Category			LIKE	@Category									+ '%'
	AND		a.LastModifiedBy	LIKE	@LastModifiedBy								+ '%'
	AND a.ApplicationMonitoredEventSourceId			 = ISNULL(@ApplicationMonitoredEventSourceId, a.ApplicationMonitoredEventSourceId )
	AND a.ApplicationMonitoredEventProcessingStateId = ISNULL(@ApplicationMonitoredEventProcessingStateId, a.ApplicationMonitoredEventProcessingStateId )
	AND a.ApplicationMonitoredEventId				 = ISNULL(@ApplicationMonitoredEventId, a.ApplicationMonitoredEventId )
	AND a.ApplicationId								 = ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.ApplicationMonitoredEventId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ApplicationMonitoredEventId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ApplicationMonitoredEventId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ApplicationMonitoredEventId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ApplicationMonitoredEventId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId	
	
	SELECT 	a.ApplicationMonitoredEventId
		,	a.ApplicationMonitoredEventSourceId			
		,	a.ApplicationMonitoredEventProcessingStateId	
		,	a.ReferenceId	
		,	a.ApplicationId								
		,	a.ReferenceCode								
		,	a.Category	
		,	a.Message 		
		,	a.IsDuplicate		
		,	a.LastModifiedBy	
		,	a.LastModifiedOn											
		,	a.ApplicationMonitoredEventSource
		,	a.ApplicationMonitoredEventProcessingState
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.ApplicationMonitoredEventId	= b.ApplicationMonitoredEventId
	ORDER BY	a.ApplicationMonitoredEventId
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
				,	a.ApplicatioApplicationMonitoredEventIdnUserTitleId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventId
		,	@SystemEntityType		= @SystemEntityType	
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO
	

