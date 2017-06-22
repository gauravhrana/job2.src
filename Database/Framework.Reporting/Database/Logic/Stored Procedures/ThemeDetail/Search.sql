IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ThemeDetailSearch')
BEGIN
	PRINT 'Dropping Procedure ThemeDetailSearch'
	DROP Procedure ThemeDetailSearch
END
GO

PRINT 'Creating Procedure ThemeDetailSearch'
GO

/******************************************************************************
**		File: 
**		Name: ThemeDetailSearch
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
			EXEC ThemeDetailSearch NULL	, NULL	, NULL
			EXEC ThemeDetailSearch NULL	, 'K'	, NULL
			EXEC ThemeDetailSearch 1		, 'K'	, NULL
			EXEC ThemeDetailSearch 1		, NULL	, NULL
			EXEC ThemeDetailSearch NULL	, NULL	, 'W'

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
Create procedure ThemeDetailSearch
(
		@ThemeDetailId			INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL	
	,	@Name					VARCHAR(50)					
	,	@ThemeId				INT						
	,	@ThemeKeyId				INT
	,	@ThemeCategoryId		INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeDetail'
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
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.ThemeDetailSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'	
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE
	
	SELECT	a.ThemeDetailId	
		,	a.ApplicationId	
		,	a.ThemeKeyId				
		,	a.Value			
		,	a.ThemeId
		,	a.ThemeCategoryId	
	INTO	#TempMain
	FROM	dbo.ThemeDetail a	
	WHERE	a.ThemeId		= ISNULL(@ThemeId, a.ThemeId )
	AND a.ThemeKeyId		= ISNULL(@ThemeKeyId, a.ThemeKeyId )
	AND a.ThemeCategoryId	= ISNULL(@ThemeCategoryId, a.ThemeCategoryId )
	AND a.ThemeDetailId		= ISNULL(@ThemeDetailId, a.ThemeDetailId )
	AND a.ApplicationId		= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.ThemeDetailId	ASC	
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ThemeDetailId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN		 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ThemeDetailId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ThemeDetailId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ThemeDetailId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId		
	
	SELECT 	a.ThemeDetailId
		,	a.ApplicationId			
		,	a.ThemeKeyId			
		,	a.Value		
		,	a.ThemeId
		,	a.ThemeCategoryId			
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.ThemeDetailId	= b.ThemeDetailId
	ORDER BY	a.ThemeDetailId
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
		ORDER BY	a.ThemeDetailId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeDetail'
		,	@EntityKey				= @ThemeDetailId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END
END
GO
	

