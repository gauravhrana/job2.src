IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='FoodTypeSearch')
BEGIN
	PRINT 'Dropping Procedure FoodTypeSearch'
	DROP Procedure FoodTypeSearch
END
GO

PRINT 'Creating Procedure FoodTypeSearch'
GO

/******************************************************************************
**		File: 
**		Name: FoodTypeSearch
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
			EXEC FoodTypeSearch NULL	, NULL	, NULL
			EXEC FoodTypeSearch NULL	, 'K'	, NULL
			EXEC FoodTypeSearch 1		, 'K'	, NULL
			EXEC FoodTypeSearch 1		, NULL	, NULL
			EXEC FoodTypeSearch NULL	, NULL	, 'W'

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

Create Procedure dbo.FoodTypeSearch
( 
		@FoodTypeId		    INT				= NULL
	,	@ApplicationId		INT				= NULL
	,	@Name				VARCHAR(50)		= NULL
	,	@Description		VARCHAR(500)	= NULL
	,	@SortOrder			INT				= NULL
	,   @AuditId			INT		
	,   @AuditDate			DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'FoodType'
)	
AS
BEGIN
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'FoodTypeId' + ', ' + 'Name'  + ', ' + '@Description'
	SET @InputValuesLocal			= CAST(@FoodTypeId AS VARCHAR(50)) + ', '+ ISNULL(@Name, 'NULL') + ', '+ ISNULL(@Description, 'NULL') 

	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.FoodTypeSearch'	
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
	
	SELECT		a.FoodTypeId
			,	a.ApplicationId		
			,	a.Name				
			,	a.Description			
			,	a.SortOrder	
	INTO		#TempMain		
	FROM		dbo.FoodType a	
	WHERE		a.Name			LIKE @Name + '%'
	AND			a.Description	LIKE @Description + '%'
	AND			a.FoodTypeId		= ISNULL(@FoodTypeId, a.FoodTypeId)
	AND			a.ApplicationId		= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.SortOrder		ASC
		,		a.Name			ASC
		,		a.FoodTypeId	ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
			ON	c.EntityKey			= a.FoodTypeId
			AND c.SystemEntityId	= @SystemEntityTypeId
			AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.FoodTypeId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.FoodTypeId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.FoodTypeId	
			,	a.ApplicationId	
			,	a.Name			
			,	a.Description		
			,	a.SortOrder	
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
		  ON	a.FoodTypeId	= b.FoodTypeId
	ORDER BY	a.SortOrder				ASC
			,	a.FoodTypeId

	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @FoodTypeId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
