IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='UseCaseSearch')
BEGIN
	PRINT 'Dropping Procedure UseCaseSearch'
	DROP Procedure UseCaseSearch
END
GO

PRINT 'Creating Procedure UseCaseSearch'
GO

/******************************************************************************
**		File: 
**		Name: UseCaseSearch
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
			EXEC UseCaseSearch NULL	 , NULL	, NULL
			EXEC UseCaseSearch NULL	 , 'K'	, NULL
			EXEC UseCaseSearch 1	 , 'K'	, NULL
			EXEC UseCaseSearch 1	 , NULL	, NULL
			EXEC UseCaseSearch NULL	 , NULL	, 'W'

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

Create procedure dbo.UseCaseSearch
(
		@UseCaseId				INT				= NULL 
	,	@ApplicationId			INT			    = NULL						
	,	@Name					VARCHAR(50)		= NULL 
	,	@Description			VARCHAR(500)	= NULL 			
	,	@TimeOfExecution		VARCHAR(50)		= NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'UseCase'		
	,	@ProjectId				INT			    = NULL			
	,	@ClientId				INT				= NULL			
	,	@NeedId					INT			    = NULL			
	,	@AuditId				INT								
	,	@AuditDate				DATETIME		= NULL			
)
AS
BEGIN
	
	-- TRACE
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'UseCaseId' + ', ' + 'Name' 
	SET @InputValuesLocal			=  CAST(@UseCaseId AS VARCHAR(50)) + ', ' + @Name 
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.UseCaseSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
			
-- TRACE

	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

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

	SELECT	a.UseCaseId
		,	a.ApplicationId		
		,	a.Name			
		,	a.Description		
		,	a.SortOrder
	INTO #TempMain
	FROM		dbo.UseCase		a
	WHERE	a.Name				LIKE @Name	+ '%'
	AND		a.Description		LIKE @Description + '%'
	AND		a.UseCaseId			= ISNULL(@UseCaseId, a.UseCaseId)
	AND		a.ApplicationId		= ISNULL(@ApplicationId, a.ApplicationId)
    
	ORDER BY	a.SortOrder		ASC
		,		a.Name			ASC
		,		a.UseCaseId		ASC


	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a	
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.UseCaseId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	


	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.UseCaseId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.UseCaseId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId

	-- Show full details
	SELECT 		a.UseCaseId
			,	a.ApplicationId		
			,	a.Name			
			,	a.Description		
			,	a.SortOrder
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.UseCaseId	= b.UseCaseId
	ORDER BY	a.SortOrder				ASC
			,	a.UseCaseId


-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'UseCase'
		,	@EntityKey				= @UseCaseId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
	

