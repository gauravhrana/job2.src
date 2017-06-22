IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='DeveloperRoleSearch')
BEGIN
	PRINT 'Dropping Procedure DeveloperRoleSearch'
	DROP Procedure DeveloperRoleSearch
END
GO

PRINT 'Creating Procedure DeveloperRoleSearch'
GO

/******************************************************************************
**		File: 
**		Name: DeveloperRoleSearch
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
			EXEC DeveloperRoleSearch NULL	, NULL	, NULL
			EXEC DeveloperRoleSearch NULL	, 'K'	, NULL
			EXEC DeveloperRoleSearch 1		, 'K'	, NULL
			EXEC DeveloperRoleSearch 1		, NULL	, NULL
			EXEC DeveloperRoleSearch NULL	, NULL	, 'W'

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
Create procedure DeveloperRoleSearch
(
		@DeveloperRoleId		INT				= NULL 	
	,	@ApplicationId				INT				= NULL
	,	@Application				VARCHAR(50)		= NULL 		
	,	@Name						VARCHAR(50)		= NULL 			
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'DeveloperRole'
	,	@ApplicationMode			INT				= NULL		
	,	@AddAuditInfo				INT				 = 1
	,	@AddTraceInfo				INT				 = 0
	,	@ReturnAuditInfo			INT				 = 0	
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
			@Name						= 'dbo.DeveloperRoleSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'	
	END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the DeveloperRole did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END

	--if blank, then assume search on all possiblities ('%')
	IF  @Application IS NULL OR LEN(RTRIM(LTRIM(@Application))) = 0
	BEGIN
		SET	@Application = '%'
	END
	
	
	SELECT	a.DeveloperRoleId	
		,	a.ApplicationId	 
		,	b.Name 'Application'
		,	a.Name				
		,	a.Description			
		,	a.SortOrder	
	INTO	#TempMain
	FROM	dbo.DeveloperRole a
	INNER JOIN	AuthenticationAndAuthorization.dbo.Application	b	ON	a.ApplicationId	= b.ApplicationId
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.DeveloperRoleId		= ISNULL(@DeveloperRoleId, a.DeveloperRoleId )
	AND a.ApplicationId				= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.SortOrder				ASC,
			 a.Name						ASC,
			 a.DeveloperRoleId	ASC			 
	
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE DeveloperRoleId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.DeveloperRoleId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.DeveloperRoleId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.DeveloperRoleId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId		
	
	SELECT 	a.DeveloperRoleId
		,	a.ApplicationId	
		,	a.Application		
		,	a.Name			
		,	a.Description		
		,	a.SortOrder			
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.DeveloperRoleId	= b.DeveloperRoleId
	ORDER BY	a.SortOrder				ASC
			,	a.DeveloperRoleId
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
				,	a.DeveloperRoleId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DeveloperRole'
		,	@EntityKey				= @DeveloperRoleId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END
END
GO
	

