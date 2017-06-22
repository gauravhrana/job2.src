IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='EntityOwnerSearch')
BEGIN
	PRINT 'Dropping Procedure EntityOwnerSearch'
	DROP Procedure EntityOwnerSearch
END
GO

PRINT 'Creating Procedure EntityOwnerSearch'
GO

/******************************************************************************
**		File: 
**		Name: EntityOwnerSearch
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
			EXEC EntityOwnerSearch NULL	, NULL	, NULL
			EXEC EntityOwnerSearch NULL	, 'K'	, NULL
			EXEC EntityOwnerSearch 1		, 'K'	, NULL
			EXEC EntityOwnerSearch 1		, NULL	, NULL
			EXEC EntityOwnerSearch NULL	, NULL	, 'W'

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
Create procedure EntityOwnerSearch
(
		@EntityOwnerId				INT				= NULL 	
	,	@ApplicationId				INT				= NULL		
	,	@EntityId					INT				= NULL
	,	@DeveloperRoleId			INT				= NULL	
	,	@Developer					VARCHAR(50)		= NULL				
	,	@FeatureOwnerStatusId		INT				= NULL	
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'EntityOwner'
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
			@Name						= 'dbo.EntityOwnerSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'	
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the EntityOwner did not provide any values
	-- assume search on all possiblities ('%')
	SET @Developer	= ISNULL(@Developer, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Developer))) = 0
		BEGIN
			SET	@Developer = '%'
		END
	
	SELECT	a.EntityOwnerId			
		,	a.ApplicationId
		,	a.EntityId
		,	a.DeveloperRoleId						
		,	a.Developer			
		,	a.FeatureOwnerStatusId	
		,	b.EntityName			AS	'Entity'
		,	c.Name					AS	'DeveloperRole'
		,	d.Name					AS	'FeatureOwnerStatus'
	INTO	#TempMain
	FROM		dbo.EntityOwner		a
	INNER JOIN	Configuration.dbo.SystemEntityType			b
		ON	a.EntityId			=	b.SystemEntityTypeId
	INNER JOIN	dbo.DeveloperRole	c
		ON	a.DeveloperRoleId	=	c.DeveloperRoleId
	INNER JOIN	dbo.FeatureOwnerStatus	d
		ON	a.FeatureOwnerStatusId	=	d.FeatureOwnerStatusId
	WHERE	a.Developer LIKE @Developer	+ '%'
	AND		a.EntityId				= ISNULL(@EntityId, a.EntityId )
	AND		a.FeatureOwnerStatusId	= ISNULL(@FeatureOwnerStatusId, a.FeatureOwnerStatusId )
	AND		a.DeveloperRoleId		= ISNULL(@DeveloperRoleId, a.DeveloperRoleId )
	AND		a.EntityOwnerId			= ISNULL(@EntityOwnerId, a.EntityOwnerId )
	AND		a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.EntityOwnerId	ASC			
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE EntityOwnerId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.EntityOwnerId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.EntityOwnerId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.EntityOwnerId
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
				ON	a.EntityOwnerId	= b.EntityOwnerId
	ORDER BY	a.FeatureOwnerStatusId				ASC
			,	a.EntityOwnerId
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
		ORDER BY	a.EntityId			ASC
				,	a.EntityOwnerId
	END
	IF @AddAuditInfo = 1 
	BEGIN

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'EntityOwner'
		,	@EntityKey				= @EntityOwnerId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END
	
END
GO
	

