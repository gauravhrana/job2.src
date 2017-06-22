IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='QuickPaginationRunSearch')
BEGIN
	PRINT 'Dropping Procedure QuickPaginationRunSearch'
	DROP Procedure QuickPaginationRunSearch
END
GO

PRINT 'Creating Procedure QuickPaginationRunSearch'
GO

/******************************************************************************
**		File: 
**		Name: QuickPaginationRunSearch
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
			EXEC QuickPaginationRunSearch NULL	, NULL	, NULL
			EXEC QuickPaginationRunSearch NULL	, 'K'	, NULL
			EXEC QuickPaginationRunSearch 1		, 'K'	, NULL
			EXEC QuickPaginationRunSearch 1		, NULL	, NULL
			EXEC QuickPaginationRunSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				WhereClause:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure QuickPaginationRunSearch
(
		@QuickPaginationRunId		INT				= NULL 	
	,	@ApplicationId				INT				= NULL
	,	@SystemEntityTypeId			INT				= NULL
	,	@ApplicationUserId			INT				= NULL		
	,	@SortClause					VARCHAR(50)		= NULL
	,	@WhereClause				VARCHAR(500)	= NULL 
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'QuickPaginationRun'
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
	SET @InputValuesLocal			= @SortClause  
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.QuickPaginationRunSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'	
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityId AS INT
	Select @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the QuickPaginationRun did not provide any values
	-- assume search on all possiblities ('%')
	SET @SortClause	= ISNULL(@SortClause, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@SortClause))) = 0
		BEGIN
			SET	@SortClause = '%'
		END
		
	SET @WhereClause	= ISNULL(@WhereClause, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@WhereClause))) = 0
		BEGIN
			SET	@WhereClause = '%'
		END
	
	SELECT	a.QuickPaginationRunId			
		,	a.ApplicationId		
		,	a.ApplicationUserId	
		,	a.SystemEntityTypeId
		,	a.SortClause						
		,	a.WhereClause	
		,	a.ExpirationTime
		,	b.EntityName			AS	'SystemEntityType'
		,	c.ApplicationUserName	AS	'ApplicationUserName'	
	INTO	#TempMain
	FROM	dbo.QuickPaginationRun a 
	INNER JOIN	Configuration.dbo.SystemEntityType b
		ON	a.SystemEntityTypeId = b.SystemEntityTypeId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	c
		ON	a.ApplicationUserId = c.ApplicationUserId
	WHERE	a.SortClause LIKE @SortClause	+ '%'
	AND		a.WhereClause LIKE @WhereClause + '%'
	AND		a.QuickPaginationRunId		= ISNULL(@QuickPaginationRunId, a.QuickPaginationRunId )
	AND		a.SystemEntityTypeId		= ISNULL(@SystemEntityTypeId, a.SystemEntityTypeId )
	AND		a.ApplicationId				= ISNULL(@ApplicationId, a.ApplicationId )
	AND		a.ApplicationUserId			<= ISNULL(@ApplicationUserId, a.ApplicationUserId)
	ORDER BY a.QuickPaginationRunId		ASC,
			 a.ExpirationTime			ASC,
			 a.SystemEntityTypeId		ASC		
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE QuickPaginationRunId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN	 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.QuickPaginationRunId
				AND c.SystemEntityId	= @SystemEntityId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.QuickPaginationRunId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.QuickPaginationRunId
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
				ON	a.QuickPaginationRunId	= b.QuickPaginationRunId
	ORDER BY	a.ApplicationUserId				ASC
			,	a.QuickPaginationRunId
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
				,	a.QuickPaginationRunId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'QuickPaginationRun'
		,	@EntityKey				= @QuickPaginationRunId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END

END
GO
	

