IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='SearchKeySearch')
BEGIN
	PRINT 'Dropping Procedure SearchKeySearch'
	DROP Procedure SearchKeySearch
END
GO

PRINT 'Creating Procedure SearchKeySearch'
GO

/******************************************************************************
**		File: 
**		Name: SearchKeySearch
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
			EXEC SearchKeySearch NULL	, NULL	, NULL
			EXEC SearchKeySearch NULL	, 'K'	, NULL
			EXEC SearchKeySearch 1		, 'K'	, NULL
			EXEC SearchKeySearch 1		, NULL	, NULL
			EXEC SearchKeySearch NULL	, NULL	, 'W'

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
Create procedure SearchKeySearch
(
		@SearchKeyId				INT				= NULL 	
	,	@ApplicationId				INT				= NULL
	,	@View						VARCHAR(100)	= NULL
	,	@Name						VARCHAR(50)		= NULL 			
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'SearchKey'
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
			@Name						= 'dbo.SearchKeySearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityId AS INT
	Select @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the SearchKey did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END
	
	SELECT	a.SearchKeyId
		,	a.ApplicationId					
		,	a.Name						
		,	a.[Description]					
		,	a.SortOrder
		,	a.[View]				
	INTO	#TempMain
	FROM		dbo.SearchKey a		
	WHERE	a.Name LIKE @Name	+ '%'
	AND		a.SearchKeyId		  = ISNULL(@SearchKeyId, a.SearchKeyId )
	AND		a.[View]  LIKE @View	+ '%'
	AND		a.ApplicationId		   = ISNULL(@ApplicationId, a.ApplicationId )	
	ORDER BY a.SortOrder		ASC,			 
			 a.SearchKeyId		ASC		
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE SearchKeyId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN	 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.SearchKeyId
				AND c.SystemEntityId	= @SystemEntityId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.SearchKeyId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.SearchKeyId
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
				ON	a.SearchKeyId	= b.SearchKeyId
	ORDER BY	a.SortOrder				ASC
			,	a.SearchKeyId
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
				,	a.SearchKeyId
	END
	IF @AddAuditInfo = 1 
	BEGIN

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SearchKey'
		,	@EntityKey				= @SearchKeyId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END
	
END
GO
	

