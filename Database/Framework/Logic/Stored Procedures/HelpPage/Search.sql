IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='HelpPageSearch')
BEGIN
	PRINT 'Dropping Procedure HelpPageSearch'
	DROP Procedure HelpPageSearch
END
GO

PRINT 'Creating Procedure HelpPageSearch'
GO

/******************************************************************************
**		File: 
**		Name: HelpPageSearch
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
			EXEC HelpPageSearch NULL	, NULL	, NULL
			EXEC HelpPageSearch NULL	, 'K'	, NULL
			EXEC HelpPageSearch 1		, 'K'	, NULL
			EXEC HelpPageSearch 1		, NULL	, NULL
			EXEC HelpPageSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Content:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure HelpPageSearch
(
		@HelpPageId					INT				= NULL 	
	,	@ApplicationId				INT				= NULL				
	,	@SystemEntityTypeId			INT				= NULL				
	,	@HelpPageContextId			INT				= NULL			
	,	@Name						VARCHAR(50)		= NULL 
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'HelpPage'
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
			@Name						= 'dbo.HelpPageSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'	
	END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityId AS INT
	Select @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the HelpPage did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END
	
	SELECT	a.HelpPageId			
		,	a.ApplicationId
		,	a.Name						
		,	a.Content			
		,	a.SortOrder	
		,	a.SystemEntityTypeId	
		,	a.HelpPageContextId
		,	b.EntityName			AS	'SystemEntityType'
		,	c.Name					AS	'HelpPageContext'
	INTO	#TempMain
	FROM		dbo.HelpPage						a
	INNER JOIN	Configuration.dbo.SystemEntityType	b	ON	a.SystemEntityTypeId	=	b.SystemEntityTypeId
	INNER JOIN	dbo.HelpPageContext					c	ON	a.HelpPageContextId		=	c.HelpPageContextId
	WHERE	a.Name LIKE @Name	+ '%'
	AND		a.HelpPageId			=	ISNULL(@HelpPageId, a.HelpPageId )
	AND		a.SystemEntityTypeId	=	ISNULL(@SystemEntityTypeId, a.SystemEntityTypeId )
	AND		a.HelpPageContextId		=	ISNULL(@HelpPageContextId, a.HelpPageContextId)
	AND		a.ApplicationId			=	ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.SortOrder		ASC,
			 a.HelpPageId		ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE HelpPageId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN
			 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.HelpPageId
				AND c.SystemEntityId	= @SystemEntityId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.HelpPageId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.HelpPageId
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
				ON	a.HelpPageId	= b.HelpPageId
	ORDER BY	a.SortOrder				ASC
			,	a.HelpPageId
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
				,	a.HelpPageId
	END
	IF @AddAuditInfo = 1 
	BEGIN

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HelpPageId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END	
END
GO
