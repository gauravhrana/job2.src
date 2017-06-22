IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationRoleSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleSearch'
	DROP  Procedure  ApplicationRoleSearch
END
GO

PRINT 'Creating Procedure ApplicationRoleSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRoleSearch
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
			EXEC ApplicationRoleSearch NULL	, NULL	, NULL
			EXEC ApplicationRoleSearch NULL	, 'K'	, NULL
			EXEC ApplicationRoleSearch 1	, 'K'	, NULL
			EXEC ApplicationRoleSearch 1	, NULL	, NULL
			EXEC ApplicationRoleSearch NULL	, NULL	, 'W'

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
Create procedure dbo.ApplicationRoleSearch
(
		@ApplicationRoleId	INT				= NULL 		
	,	@ApplicationId		INT				= NULL 		
	,	@Name				VARCHAR(50)		= NULL		
	,	@AuditId			INT							
	,	@AuditDate			DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50) = 'ApplicationRole'
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
	-- TRACE AND LOGGING ---
	
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'Name' 
	SET @InputValuesLocal			= @Name  
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.ApplicationRoleSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'
	END	

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the ApplicationRole did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END	
	
	SELECT	a.ApplicationRoleId	
		,	a.ApplicationId	
		,	a.Name				
		,	a.Description			
		,	a.SortOrder	
		,	b.Name			AS	'Application'
	INTO		#TempMain
	FROM		dbo.ApplicationRole a	
	INNER JOIN	dbo.Application		b ON a.ApplicationId = b.ApplicationId
	WHERE	a.Name LIKE @Name	+ '%'
	AND		a.ApplicationRoleId		= ISNULL(@ApplicationRoleId, a.ApplicationRoleId )
	AND		a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.SortOrder			ASC,
			 a.Name					ASC,
			 a.ApplicationRoleId	ASC
    IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ApplicationRoleId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ApplicationRoleId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ApplicationRoleId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ApplicationRoleId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId	
	
	SELECT 	a.ApplicationRoleId
		,	a.ApplicationId			
		,	a.Name			
		,	a.Description		
		,	a.SortOrder		
		,	a.Application	
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.ApplicationRoleId	= b.ApplicationRoleId
	ORDER BY	a.SortOrder				ASC
			,	a.ApplicationRoleId
      END
	ELSE
	BEGIN
		SELECT 	a.*
			, 	UpdatedDate = '1/1/1900'
			,	UpdatedBy	= 'Unknown'
			,	LastAction	= 'Unknown'
		FROM	#TempMain a		
		ORDER BY	a.SortOrder				ASC
				,	a.ApplicationRoleId
	END
	IF @AddAuditInfo = 1 
	BEGIN

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @ApplicationRoleId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

	END
		
END
GO

