IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ModuleOwnerSearch')
BEGIN
	PRINT 'Dropping Procedure ModuleOwnerSearch'
	DROP Procedure ModuleOwnerSearch
END
GO

PRINT 'Creating Procedure ModuleOwnerSearch'
GO

/******************************************************************************
**		File: 
**		Name: ModuleOwnerSearch
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
			EXEC ModuleOwnerSearch NULL	, NULL	, NULL
			EXEC ModuleOwnerSearch NULL	, 'K'	, NULL
			EXEC ModuleOwnerSearch 1	, 'K'	, NULL
			EXEC ModuleOwnerSearch 1	, NULL	, NULL
			EXEC ModuleOwnerSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Developer:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure ModuleOwnerSearch
(
		@ModuleOwnerId				INT				= NULL 	
	,	@ApplicationId				INT				= NULL		
	,	@ModuleId					INT				= NULL
	,	@DeveloperRoleId			INT				= NULL	
	,	@Developer					VARCHAR(50)		= NULL				
	,	@FeatureOwnerStatusId		INT				= NULL	
	,	@TotalHoursWorked			INT				= NULL
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'ModuleOwner'
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
	SET @InputParametersLocal		= 'Developer' 
	SET @InputValuesLocal			= @Developer 
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.ModuleOwnerSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'	
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the ModuleOwner did not provide any values
	-- assume search on all possiblities ('%')
	SET @Developer	= ISNULL(@Developer, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Developer))) = 0
		BEGIN
			SET	@Developer = '%'
		END
	
	SELECT	a.ModuleOwnerId			
		,	a.ApplicationId
		,	a.ModuleId
		,	a.DeveloperRoleId						
		,	a.Developer			
		,	a.FeatureOwnerStatusId
		,	a.TotalHoursWorked	
		,	b.Name					AS	'Module'
		,	c.Name					AS	'DeveloperRole'
		,	d.Name					AS	'FeatureOwnerStatus'
	INTO	#TempMain
	FROM		dbo.ModuleOwner		a
	INNER JOIN	dbo.Module			b
		ON	a.ModuleId			=	b.ModuleId
	INNER JOIN	dbo.DeveloperRole	c
		ON	a.DeveloperRoleId	=	c.DeveloperRoleId
	INNER JOIN	dbo.FeatureOwnerStatus	d
		ON	a.FeatureOwnerStatusId	=	d.FeatureOwnerStatusId
	WHERE	a.Developer LIKE @Developer --	+ '%'
	AND		a.ModuleId				= ISNULL(@ModuleId, a.ModuleId )
	AND		a.FeatureOwnerStatusId	= ISNULL(@FeatureOwnerStatusId, a.FeatureOwnerStatusId )
	AND		a.DeveloperRoleId		= ISNULL(@DeveloperRoleId, a.DeveloperRoleId )
	AND		a.ModuleOwnerId			= ISNULL(@ModuleOwnerId, a.ModuleOwnerId )
	AND		a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.ModuleOwnerId	ASC			 
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ModuleOwnerId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ModuleOwnerId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ModuleOwnerId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ModuleOwnerId
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
				ON	a.ModuleOwnerId	= b.ModuleOwnerId
	ORDER BY	a.FeatureOwnerStatusId				ASC
			,	a.ModuleOwnerId
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
				,	a.ModuleOwnerId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ModuleOwner'
		,	@EntityKey				= @ModuleOwnerId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END
END
GO
	

