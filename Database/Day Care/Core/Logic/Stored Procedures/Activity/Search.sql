IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ActivitySearch')
BEGIN
	PRINT 'Dropping Procedure ActivitySearch'
	DROP Procedure ActivitySearch
END
GO

PRINT 'Creating Procedure ActivitySearch'
GO

/******************************************************************************
**		File: 
**		Name: ActivitySearch
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
			EXEC ActivitySearch NULL	, NULL	, NULL
			EXEC ActivitySearch NULL	, 'K'	, NULL
			EXEC ActivitySearch 1		, 'K'	, NULL
			EXEC ActivitySearch 1		, NULL	, NULL
			EXEC ActivitySearch NULL	, NULL	, 'W'

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

Create Procedure dbo.ActivitySearch
(
	    @ActivityId		    INT				= NULL
	,	@ApplicationId		INT				= NULL  
	,	@StudentId			INT				= NULL
	,	@ActivityTypeId     INT				= NULL
	,	@ActivitySubTypeId  INT				= NULL
	,   @AuditId			INT		
	,   @AuditDate			DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'Activity'
)	
AS
BEGIN			
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'ActivityId' + ', ' + 'StudentId' 
	SET @InputValuesLocal			= CAST(@ActivityId As VARCHAR(50)) + ', ' + CAST(@StudentId As VARCHAR(50))
	EXEC dbo.StoredProcedureLogInsert	@Name						= 'dbo.ActivitySearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		,	@ExecutedBy					= @AuditId	
-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType,@ApplicationId)
			
	SELECT		a.ActivityId
			,	a.ApplicationId		
			,	a.StudentId				
			,	a.ActivityTypeId		
			,	a.ActivitySubTypeId	
	INTO		#TempMain		
	FROM		dbo.Activity a	
	WHERE		a.ActivityId		= ISNULL(@ActivityId, a.ActivityId)
	AND			a.StudentId			= ISNULL(@StudentId, a.StudentId)
	AND			a.ApplicationId		= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.StudentId         ASC
		,		a.ActivityId		ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ActivityId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.ActivityId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ActivityId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.ActivityId
			,	a.ApplicationId		
			,	a.StudentId				
			,	a.ActivityTypeId
			,	a.ActivitySubTypeId	
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.ActivityId	= b.ActivityId
	ORDER BY	a.StudentId		  ASC
			,	a.ActivityId      ASC
									
	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert					
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ActivityId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
