IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='MenuDisplayNameSearch')
BEGIN
	PRINT 'Dropping Procedure MenuDisplayNameSearch'
	DROP Procedure MenuDisplayNameSearch
END
GO

PRINT 'Creating Procedure MenuDisplayNameSearch'
GO
/******************************************************************************
**		File: 
**		Name: MenuDisplayNameSearch
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
			EXEC MenuDisplayNameSearch NULL	, NULL	, NULL
			EXEC MenuDisplayNameSearch NULL	, 'K'	, NULL
			EXEC MenuDisplayNameSearch 1	, 'K'	, NULL
			EXEC MenuDisplayNameSearch 1	, NULL	, NULL
			EXEC MenuDisplayNameSearch NULL	, NULL	, 'W'

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
Create Procedure dbo.MenuDisplayNameSearch
(
		@MenuDisplayNameId		INT			= NULL	
	,	@ApplicationId			INT			= NULL
	,	@Value					VARCHAR(50)	= NULL
	,	@LanguageId				INT			= NULL			
	,	@MenuId					INT			= NULL
	,	@IsLanguage				INT			= NULL	
	,	@IsDefault				INT			= NULL	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50) = 'MenuDisplayName'
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
	SET @InputParametersLocal		= 'Value'
	SET @InputValuesLocal			= @Value
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.MenuDisplayNameSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		END
	-- if the DataType did not provide any values
	-- assume search on all possiblities ('%')
	SET	@Value = ISNULL(@Value,'%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@Value))) = 0 
	BEGIN
		SET	@Value = '%'
	END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)	

	SELECT	a.MenuDisplayNameId			
		,	a.ApplicationId	
		,	a.MenuId		
		,	a.LanguageId			
		,	a.Value
		,	a.IsDefault	
		,	b.Name			AS 'Menu'
		,	c.Name			AS 'Language'	
	FROM	dbo.MenuDisplayName		a
	INNER JOIN dbo.Menu	b 
		ON a.MenuId = b.MenuId 
	INNER JOIN dbo.Language			c
		ON a.LanguageId = c.LanguageId
	WHERE	a.Value						LIKE @Value	+ '%'
	AND		a.MenuDisplayNameId			= ISNULL(@MenuDisplayNameId, a.MenuDisplayNameId)
	AND		a.ApplicationId				= ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.LanguageId				= ISNULL(@LanguageId, a.LanguageId)
	AND		a.IsDefault					= ISNULL(@IsDefault, a.IsDefault)
	AND		ISNULL(a.MenuId, -1)		= ISNULL(@MenuId, ISNULL(a.MenuId, -1))
	ORDER BY a.LanguageId			ASC,
			 a.Value				ASC,
			 a.MenuDisplayNameId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE MenuDisplayNameId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN
					

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.MenuDisplayNameId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.MenuDisplayNameId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.MenuDisplayNameId
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
				ON	a.MenuDisplayNameId	= b.MenuDisplayNameId
	ORDER BY	a.LanguageId				ASC
			,	a.MenuDisplayNameId
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
				,	a.MenuDisplayNameId
	END
	IF @AddAuditInfo = 1 
	BEGIN

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuDisplayNameId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO
