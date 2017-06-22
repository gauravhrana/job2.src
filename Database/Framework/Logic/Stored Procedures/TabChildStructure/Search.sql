IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='TabChildStructureSearch')
BEGIN
	PRINT 'Dropping Procedure TabChildStructureSearch'
	DROP Procedure TabChildStructureSearch
END
GO

PRINT 'Creating Procedure TabChildStructureSearch'
GO

/******************************************************************************
**		File: 
**		Name: TabChildStructureSearch
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
			EXEC TabChildStructureSearch NULL	, NULL	, NULL
			EXEC TabChildStructureSearch NULL	, 'K'	, NULL
			EXEC TabChildStructureSearch 1		, 'K'	, NULL
			EXEC TabChildStructureSearch 1		, NULL	, NULL
			EXEC TabChildStructureSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				EntityName:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure TabChildStructureSearch
(
		@TabChildStructureId		INT				= NULL 	
	,	@ApplicationId				INT				= NULL		
	,	@Name						VARCHAR(50)		= NULL
	,	@EntityName					VARCHAR(50)		= NULL
	,	@TabParentStructureId		INT				= NULL 			
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'TabChildStructure'
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
			@Name						= 'dbo.TabChildStructureSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the TabChildStructure did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END

	SET @EntityName	= ISNULL(@EntityName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@EntityName))) = 0
		BEGIN
			SET	@EntityName = '%'
		END		
	
	SELECT	a.TabChildStructureId			
		,	a.ApplicationId
		,	a.Name						
		,	a.EntityName			
		,	a.SortOrder	
		,	a.TabParentStructureId	
		,	a.InnerControlPath
		,	b.Name					AS	'TabParentStructure'
	INTO	#TempMain
	FROM		dbo.TabChildStructure		a
	INNER JOIN	dbo.TabParentStructure		b	ON	a.TabParentStructureId	=	b.TabParentStructureId
	WHERE		a.Name						LIKE @Name	+ '%'
	AND			a.EntityName				LIKE @EntityName + '%'
	AND			a.TabChildStructureId		= ISNULL(@TabChildStructureId,		a.TabChildStructureId)
	AND			a.TabParentStructureId		= ISNULL(@TabParentStructureId,		a.TabParentStructureId)
	AND			a.ApplicationId				= ISNULL(@ApplicationId,	a.ApplicationId)
	ORDER BY a.SortOrder			ASC,
			 a.Name					ASC,
			 a.TabChildStructureId	ASC	
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE TabChildStructureId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN		 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.TabChildStructureId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.TabChildStructureId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.TabChildStructureId
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
				ON	a.TabChildStructureId	= b.TabChildStructureId
	ORDER BY	a.SortOrder				ASC
			,	a.TabChildStructureId
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
				,	a.TabChildStructureId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TabChildStructure'
		,	@EntityKey				= @TabChildStructureId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
		END
END
GO
	

