IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='MenuSearch')
BEGIN
	PRINT 'Dropping Procedure MenuSearch'
	DROP Procedure MenuSearch
END
GO

PRINT 'Creating Procedure MenuSearch'
GO
/******************************************************************************
**		File: 
**		Name: MenuSearch
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
			EXEC MenuSearch NULL	, NULL	, NULL
			EXEC MenuSearch NULL	, 'K'	, NULL
			EXEC MenuSearch 1		, 'K'	, NULL
			EXEC MenuSearch 1		, NULL	, NULL
			EXEC MenuSearch NULL	, NULL	, 'W'

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
Create Procedure dbo.MenuSearch
(
		@MenuId					INT			= NULL	
	,	@ApplicationId			INT			= NULL
	,	@Name					VARCHAR(50)	= NULL	
	,	@Value					VARCHAR(50)	= NULL			
	,	@ParentMenuId			INT			= NULL	
	,	@PrimaryDeveloper 		VARCHAR(50)	= NULL
	,	@IsChecked				INT			= NULL
	,	@IsVisible				INT			= NULL	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50) = 'Menu'
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
			@Name						= 'dbo.MenuSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
	END
	-- if the DataType did not provide any values
	-- assume search on all possiblities ('%')
	SET	@Name = ISNULL(@Name,'%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@Name))) = 0 
	BEGIN
		SET	@Name = '%'
	END
	
	SET	@PrimaryDeveloper = ISNULL(@PrimaryDeveloper,'%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@PrimaryDeveloper))) = 0 
	BEGIN
		SET	@PrimaryDeveloper = '%'
	END
	 
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)	

	SELECT	a.MenuId			
		,	a.ApplicationId	
		,	a.Name		
		,	a.Value	
		,	a.ParentMenuId
		,	b.Name			AS 'ParentMenu'	
		,	a.PrimaryDeveloper	
		,	a.IsChecked		
		,	a.IsVisible		
		,	a.NavigateURL		
		,	a.Description		
		,	a.SortOrder	
		,	c.Value			AS 'MenuDisplayName'
		,	d.Name			AS 'Application'
	INTO		#TempMain
	FROM		dbo.Menu			a
	LEFT JOIN	dbo.Menu			b 
		ON a.ParentMenuId = b.MenuId
	INNER JOIN	dbo.MenuDisplayName	c
		ON	a.MenuId = c.MenuId
	INNER JOIN AuthenticationAndAuthorization.dbo.Application d
		ON a.ApplicationId = d.ApplicationId
	WHERE	(a.Name						LIKE @Name	+ '%'	OR
			 a.Description				LIKE @Name	+ '%'	OR
			 c.Value						LIKE @Name	+ '%')
	AND		a.PrimaryDeveloper			LIKE @PrimaryDeveloper	+ '%'
	AND		ISNULL(a.Value, -1)			= ISNULL(@Value, ISNULL(a.Value, -1))
	AND		a.MenuId					= ISNULL(@MenuId, a.MenuId)
	AND		a.ApplicationId				= ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.IsVisible					= ISNULL(@IsVisible, a.IsVisible)
	AND		a.IsChecked					= ISNULL(@IsChecked, a.IsChecked)
	AND		ISNULL(a.ParentMenuId, -1)	= ISNULL(@ParentMenuId, ISNULL(a.ParentMenuId, -1))
	ORDER BY a.SortOrder	ASC,
			 a.Name			ASC,
			 a.MenuId		ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE MenuId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.MenuId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.MenuId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.MenuId
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
				ON	a.MenuId	= b.MenuId
	ORDER BY	a.SortOrder				ASC
			,	a.MenuId
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
				,	a.MenuId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO
