IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='MealSearch')
BEGIN
	PRINT 'Dropping Procedure MealSearch'
	DROP Procedure MealSearch
END
GO

PRINT 'Creating Procedure MealSearch'
GO

/******************************************************************************
**		File: 
**		StudentID: MealSearch
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
			EXEC MealSearch NULL	, NULL	, NULL
			EXEC MealSearch NULL	, 'K'	, NULL
			EXEC MealSearch 1		, 'K'	, NULL
			EXEC MealSearch 1		, NULL	, NULL
			EXEC MealSearch NULL	, NULL	, 'W'

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

Create Procedure dbo.MealSearch
(
	    @MealId				INT			= NULL
	,	@ApplicationId		INT			= NULL  
	,	@StudentId			INT			= NULL
	,	@Date				DATETIME	= NULL
	,	@MealTypeId			INT			= NULL
	,   @AuditId			INT		    
	,   @AuditDate			DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Meal'
)	
AS
BEGIN			
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'MealId' + ', ' + 'StudentId' 
	SET @InputValuesLocal			= CAST(@MealId As VARCHAR(50)) + ', ' + CAST(@StudentId As VARCHAR(50))
	EXEC dbo.StoredProcedureLogInsert	@Name						= 'dbo.MealSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		,	@ExecutedBy					= @AuditId	
-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType, @ApplicationId)
			
	SELECT		a.MealId
			,	a.ApplicationId		
			,	a.StudentId				
			,	a.Date		
			,	a.MealTypeId	
	INTO		#TempMain		
	FROM		dbo.Meal a	
	WHERE		a.MealId		    = ISNULL(@MealId, a.MealId)
	AND			a.StudentId			= ISNULL(@StudentId, a.StudentId)
	AND			a.ApplicationId	    = ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.StudentId         ASC
		,		a.MealId	        ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.MealId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.MealId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.MealId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.MealId
			,	a.ApplicationId		
			,	a.StudentId				
			,	a.Date
			,	a.MealTypeId	
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.MealId	= b.MealId
	ORDER BY	a.StudentId		  ASC
			,	a.MealId          ASC
									
	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert					
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @MealId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
