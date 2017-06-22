IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='SearchKeyDetailSearch')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailSearch'
	DROP Procedure SearchKeyDetailSearch
END
GO

PRINT 'Creating Procedure SearchKeyDetailSearch'
GO

/******************************************************************************
**		File: 
**		Name: SearchKeyDetailSearch
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
			EXEC SearchKeyDetailSearch NULL	, NULL	, NULL
			EXEC SearchKeyDetailSearch NULL	, 'K'	, NULL
			EXEC SearchKeyDetailSearch 1		, 'K'	, NULL
			EXEC SearchKeyDetailSearch 1		, NULL	, NULL
			EXEC SearchKeyDetailSearch NULL	, NULL	, 'W'

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
Create procedure SearchKeyDetailSearch
(
		@SearchKeyDetailId			INT				= NULL 	
	,	@ApplicationId				INT				= NULL
	,	@SearchKeyId				INT				= NULL	
	,	@SearchParameter			VARCHAR(200)	= NULL			
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'SearchKeyDetail'
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
	SET @InputParametersLocal		= 'SearchKeyDetailId' + ', ' + 'SearchKey' 
	SET @InputValuesLocal			=  CAST(@SearchKeyDetailId AS VARCHAR(50)) + ', ' + @SearchKey 
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.SearchKeyDetailSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityId AS INT
	Select @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntityType)	
	
	--if blank, then assume search on all possiblities ('%')
	IF  @SearchParameter IS NULL OR LEN(RTRIM(LTRIM(@SearchParameter))) = 0
	BEGIN
		SET	@SearchParameter = '%'
	END	

	SELECT	a.SearchKeyDetailId
		,	a.ApplicationId	
		,	a.SearchParameter
		,	a.SearchKeyId	
		,	b.Name					AS	'SearchKey'		
		,	a.SortOrder
	FROM		dbo.SearchKeyDetail	a
	INNER JOIN	dbo.SearchKey		b	ON	a.SearchKeyId	=	b.SearchKeyId
	WHERE	a.SearchParameter	  LIKE @SearchParameter	+ '%'	
	AND a.SearchKeyId	  = ISNULL(@SearchKeyId, a.SearchKeyId)	
	AND a.ApplicationId	  = ISNULL(@ApplicationId, a.ApplicationId)	 
	ORDER BY a.SearchKeyDetailId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE SearchKeyDetailId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN
			 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.SearchKeyDetailId
				AND c.SystemEntityId	= @SystemEntityId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.SearchKeyDetailId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.SearchKeyDetailId
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
				ON	a.SearchKeyDetailId	= b.SearchKeyDetailId
	ORDER BY	a.SearchKeyDetailId
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
				,	a.SearchKeyDetailId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SearchKeyDetail'
		,	@EntityKey				= @SearchKeyDetailId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END

END
GO
	

