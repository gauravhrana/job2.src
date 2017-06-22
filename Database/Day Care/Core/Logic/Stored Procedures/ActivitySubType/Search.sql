IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ActivitySubTypeSearch')
BEGIN
	PRINT 'Dropping Procedure ActivitySubTypeSearch'
	DROP Procedure ActivitySubTypeSearch
END
GO

PRINT 'Creating Procedure ActivitySubTypeSearch'
GO

/******************************************************************************
**		File: 
**		Name: ActivitySubTypeSearch
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
			EXEC ActivitySubTypeSearch NULL	, NULL	, NULL
			EXEC ActivitySubTypeSearch NULL	, 'K'	, NULL
			EXEC ActivitySubTypeSearch 1	, 'K'	, NULL
			EXEC ActivitySubTypeSearch 1	, NULL	, NULL
			EXEC ActivitySubTypeSearch NULL	, NULL	, 'W'

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

Create Procedure dbo.ActivitySubTypeSearch
(
	    @ActivitySubTypeId	INT             = NULL
	,	@ApplicationId		INT				= NULL
	,	@ActivityTypeId	    INT             = NULL
	,	@Name				VARCHAR(50)     = NULL
	,	@Description		VARCHAR(500)	= NULL 
	,	@SortOrder			INT				= NULL
	,   @AuditId			INT					
	,   @AuditDate			DATETIME	    = NULL	
	,	@SystemEntityType	VARCHAR(50)	    = 'ActivitySubType'
)	
AS
BEGIN

	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'ActivitySubTypeId' + ', ' + 'Name'+ ', ' + '@Description'
	SET @InputValuesLocal			= CAST(@ActivitySubTypeId AS VARCHAR(50)) + ', '+ ISNULL(@Name, 'NULL') + ', '+ ISNULL(@Description, 'NULL') 

	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.ActivitySubTypeSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal

	-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType,@ApplicationId)

  --if blank, then assume search on all possiblities ('%')
	IF  @Name IS NULL OR LEN(RTRIM(LTRIM(@Name))) = 0
	BEGIN
		SET	@NAME = '%'
	END

	--if blank, then assume search on all possiblities ('%')
	IF  @Description IS NULL OR LEN(RTRIM(LTRIM(@Description))) = 0
	BEGIN
		SET	@Description = '%'
	END
	
	SELECT		a.ActivitySubTypeId
			,   a.ActivityTypeId
			,	a.ApplicationId		
			,	a.Name				
			,	a.Description			
			,	a.SortOrder	
	INTO		#TempMain		
	FROM		dbo.ActivitySubType a	
	WHERE		a.Name			LIKE @Name + '%'
	AND			a.Description	LIKE @Description + '%'
	AND			a.ApplicationId			= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.SortOrder				ASC
		,		a.Name					ASC
		,		a.ActivitySubTypeId		ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ActivitySubTypeId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.ActivitySubTypeId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ActivitySubTypeId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.ActivitySubTypeId	
			,	a.ActivityTypeId
			,	a.ApplicationId	
			,	a.Name			
			,	a.Description		
			,	a.SortOrder	
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.ActivitySubTypeId	= b.ActivitySubTypeId
	ORDER BY	a.SortOrder				ASC
			,	a.ActivitySubTypeId     ASC

	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ActivitySubTypeId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
