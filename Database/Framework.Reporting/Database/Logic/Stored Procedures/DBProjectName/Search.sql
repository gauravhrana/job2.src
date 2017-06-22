IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='DBProjectNameSearch')
BEGIN
	PRINT 'Dropping Procedure DBProjectNameSearch'
	DROP Procedure DBProjectNameSearch
END
GO

PRINT 'Creating Procedure DBProjectNameSearch'
GO

/******************************************************************************
**		File: 
**		Name: DBProjectNameSearch
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
			EXEC DBProjectNameSearch NULL	, NULL	, NULL
			EXEC DBProjectNameSearch NULL	, 'K'	, NULL
			EXEC DBProjectNameSearch 1		, 'K'	, NULL
			EXEC DBProjectNameSearch 1		, NULL	, NULL
			EXEC DBProjectNameSearch NULL	, NULL	, 'W'

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
Create procedure DBProjectNameSearch
(
		@DBProjectNameId			INT				= NULL 	
	,	@ApplicationId				INT				= NULL		
	,	@Name						VARCHAR(50)		= NULL 			
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'DBProjectName'
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
	
	-- TRACE
				
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000) 
	 
	SET @InputParametersLocal		= 'Name' 
	SET @InputValuesLocal			= @Name  

	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.DBProjectNameSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the DBProjectName did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END
	
	SELECT	a.DBProjectNameId	
		,	a.ApplicationId	
		,	a.Name				
		,	a.Description			
		,	a.SortOrder	
	INTO	#TempMain
	FROM	dbo.DBProjectName a	
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.DBProjectNameId		= ISNULL(@DBProjectNameId, a.DBProjectNameId )
	AND a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.SortOrder		ASC,
			 a.Name				ASC,
			 a.DBProjectNameId	ASC	

	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE DBProjectNameId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN		 		 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.DBProjectNameId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.DBProjectNameId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.DBProjectNameId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId		
	
	SELECT 	a.DBProjectNameId
		,	a.ApplicationId			
		,	a.Name			
		,	a.[Description]		
		,	a.SortOrder			
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.DBProjectNameId	= b.DBProjectNameId
	ORDER BY	a.SortOrder				ASC
			,	a.DBProjectNameId
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
				,	a.DBProjectNameId

	END
	
	IF @AddAuditInfo = 1 
	BEGIN

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DBProjectName'
		,	@EntityKey				= @DBProjectNameId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId		
	END

END
GO
	

