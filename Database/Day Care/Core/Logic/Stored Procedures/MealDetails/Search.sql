IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='MealDetailSearch')
BEGIN
	PRINT 'Dropping Procedure MealDetailSearch'
	DROP Procedure MealDetailSearch
END
GO

PRINT 'Creating Procedure MealDetailSearch'
GO

/******************************************************************************
**		File: 
**		Name: MealDetailSearch
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
			EXEC MealDetailSearch NULL	, NULL	, NULL
			EXEC MealDetailSearch NULL	, 'K'	, NULL
			EXEC MealDetailSearch 1		, 'K'	, NULL
			EXEC MealDetailSearch 1		, NULL	, NULL
			EXEC MealDetailSearch NULL	, NULL	, 'W'

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

Create Procedure dbo.MealDetailSearch
(
	    @MealDetailId		INT				= NULL
	,	@ApplicationId		INT				= NULL
	,	@MealId				INT				= NULL
	,	@FoodTypeId			INT				= NULL
	,	@AmtFinished	    FLOAT			= NULL	
	,   @AuditId			INT		
	,   @AuditDate			DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'MealDetail'
)	
AS
BEGIN			
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'MealDetailId' + ', ' + 'MealId' 
	SET @InputValuesLocal			= CAST(@MealDetailId As VARCHAR(50)) + ', ' + CAST(@MealId As VARCHAR(50))
	EXEC dbo.StoredProcedureLogInsert	@Name						= 'dbo.MealDetailSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		,	@ExecutedBy					= @AuditId	
-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType,@ApplicationId)
			
	SELECT		a.MealDetailId
			,	a.ApplicationId		
			,	a.MealId		
			,	a.FoodTypeId	
			,	a.AmtFinished
	INTO		#TempMain
	FROM		dbo.MealDetail a	
	WHERE		a.MealDetailId		    = ISNULL(@MealDetailId, a.MealDetailId)
	AND			a.MealId				= ISNULL(@MealId, a.MealId)
	AND			a.ApplicationId			= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.MealId         ASC
		,		a.MealDetailId	ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.MealDetailId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.MealDetailId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.MealDetailId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.MealDetailId
			,	a.ApplicationId		
			,	a.MealId		
			,	a.FoodTypeId	
			,	a.AmtFinished
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.MealDetailId	= b.MealDetailId
	ORDER BY	a.MealId		  ASC
			,	a.MealDetailId    ASC
									
	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert					
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @MealDetailId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
