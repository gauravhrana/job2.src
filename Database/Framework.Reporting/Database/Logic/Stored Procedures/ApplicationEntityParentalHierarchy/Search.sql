IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationEntityParentalHierarchySearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationEntityParentalHierarchySearch'
	DROP Procedure ApplicationEntityParentalHierarchySearch
END
GO

PRINT 'Creating Procedure ApplicationEntityParentalHierarchySearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationEntityParentalHierarchySearch
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
			EXEC ApplicationEntityParentalHierarchySearch NULL	, NULL	, NULL
			EXEC ApplicationEntityParentalHierarchySearch NULL	, 'K'	, NULL
			EXEC ApplicationEntityParentalHierarchySearch 1		, 'K'	, NULL
			EXEC ApplicationEntityParentalHierarchySearch 1		, NULL	, NULL
			EXEC ApplicationEntityParentalHierarchySearch NULL	, NULL	, 'W'

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
Create procedure ApplicationEntityParentalHierarchySearch
(
		@ApplicationEntityParentalHierarchyId	INT				= NULL 	
	,	@ApplicationId							INT				= NULL
	,	@Name									VARCHAR(50)		= NULL 			
	,	@AuditId								INT								
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationEntityParentalHierarchy'	
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	IF @AddTraceInfo = 1 
	BEGIN
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'Name' 
	SET @InputValuesLocal			= @Name
	EXEC TaskTimeTracker.dbo.StoredProcedureLogInsert
			@Name					= 'dbo.ApplicationEntityParentalHierarchySearch'	
		,	@InputParameters		= @InputParametersLocal
		,	@InputValues			= @InputValuesLocal	
	END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END
	
	SELECT	a.ApplicationEntityParentalHierarchyId	
		,	a.ApplicationId	
		,	a.Name				
		,	a.Description			
		,	a.SortOrder	
	INTO	#TempMain
	FROM	dbo.ApplicationEntityParentalHierarchy a		
	WHERE	a.Name LIKE @Name	+ '%'
	AND		a.ApplicationEntityParentalHierarchyId =
			CASE
				WHEN @ApplicationEntityParentalHierarchyId IS NULL THEN a.ApplicationEntityParentalHierarchyId
				ELSE @ApplicationEntityParentalHierarchyId
			END	
	AND		a.ApplicationId =
			CASE
				WHEN @ApplicationId IS NULL THEN a.ApplicationId
				ELSE @ApplicationId
			END		
	ORDER BY a.SortOrder	ASC
		,	 a.Name			ASC
		,	 a.ApplicationEntityParentalHierarchyId	ASC			
	 IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ApplicationEntityParentalHierarchyId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN 
	
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ApplicationEntityParentalHierarchyId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ApplicationEntityParentalHierarchyId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ApplicationEntityParentalHierarchyId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId		
	
	SELECT 	a.ApplicationEntityParentalHierarchyId	
		,	a.ApplicationId	
		,	a.Name			
		,	a.Description		
		,	a.SortOrder			
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.ApplicationEntityParentalHierarchyId	= b.ApplicationEntityParentalHierarchyId
	ORDER BY	a.SortOrder				ASC
			,	a.ApplicationEntityParentalHierarchyId
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
				,	a.ApplicationEntityParentalHierarchyId
	END
	IF @AddAuditInfo = 1 
	BEGIN


	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationEntityParentalHierarchyId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO
	

