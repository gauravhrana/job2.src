IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ReportSearch')
BEGIN
	PRINT 'Dropping Procedure ReportSearch'
	DROP Procedure ReportSearch
END
GO

PRINT 'Creating Procedure ReportSearch'
GO

/******************************************************************************
**		File: 
**		Name: ReportSearch
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
			EXEC ReportSearch NULL	, NULL	, NULL
			EXEC ReportSearch NULL	, 'K'	, NULL
			EXEC ReportSearch 1		, 'K'	, NULL
			EXEC ReportSearch 1		, NULL	, NULL
			EXEC ReportSearch NULL	, NULL	, 'W'

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
Create procedure dbo.ReportSearch
(
		@ReportId				INT				= NULL 
	,	@ApplicationId			INT				= NULL
	,	@Application			VARCHAR(50)		= NULL 			
	,	@Name					VARCHAR(50)		= NULL
	,	@Title					VARCHAR(50)		= NULL	
	,	@Description			VARCHAR(500)	= NULL	
	,	@ModifiedDate			DATETIME		= NULL
	,	@CreatedDate			DATETIME		= NULL
	,	@CreatedByAuditId		INT				= NULL
	,	@ModifiedByAuditId		INT				= NULL
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType    	VARCHAR(50)		= 'Report'	
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0		
)
WITH RECOMPILE
AS
BEGIN	
	
	SET  NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN

			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'ReportId' + ', ' + 'Name'  
			SET @InputValuesLocal			= CAST(@ReportId AS VARCHAR(50)) + ', ' + ISNULL(@Name, 'NULL') + ', '+ ISNULL(@Description, 'NULL') + ', '+ ISNULL(@Title, 'NULL')
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.ReportSearch'	
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal
		
		END	

		
			
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	--if blank, then assume search on all possiblities ('%')
	IF  @Name IS NULL OR LEN(RTRIM(LTRIM(@Name))) = 0
	BEGIN
		SET	@NAME = '%'
	END

	--if blank, then assume search on all possiblities ('%')
	IF  @Description IS NULL OR LEN(RTRIM(LTRIM(@Description))) = 0
	BEGIN
		SET	@Description = '%'
	END

	--if blank, then assume search on all possiblities ('%')
	IF  @Title IS NULL OR LEN(RTRIM(LTRIM(@Title))) = 0
	BEGIN
		SET	@Title ='%'
	END
	
	--if blank, then assume search on all possiblities ('%')
	IF  @Application IS NULL OR LEN(RTRIM(LTRIM(@Description))) = 0
	BEGIN
		SET	@Application = '%'
	END
	

	SELECT	a.ReportId	
		,	a.ApplicationId	 
		,	b.Name 'Application'
		,	a.Name	
		,	a.Title						
		,	a.Description					
		,	a.SortOrder	
		,	a.CreatedDate
		,	a.ModifiedDate
		,	a.CreatedByAuditId 
		,	a.ModifiedByAuditId
	INTO		#TempMain							
	FROM		dbo.Report	a
	INNER JOIN	AuthenticationAndAuthorization.dbo.Application	b	ON	a.ApplicationId	= b.ApplicationId
	WHERE	a.Name					LIKE @Name + '%'
	AND		a.Description			LIKE @Description + '%'	
	AND		a.Title					LIKE @Title + '%'	
	AND		a.ReportId				= ISNULL(@ReportId, a.ReportId)	
	AND		a.ApplicationId			= ISNULL(@ApplicationId	, a.ApplicationId)
	AND		a.CreatedDate			= ISNULL(@CreatedDate, a.CreatedDate)
	AND		a.ModifiedDate			= ISNULL(@ModifiedDate, a.ModifiedDate)
	AND		a.CreatedByAuditId		= ISNULL(@CreatedByAuditId, a.CreatedByAuditId)
	AND		a.ModifiedByAuditId		= ISNULL(@ModifiedByAuditId, a.ModifiedByAuditId)	
	ORDER BY	a.SortOrder	ASC
		,		a.Name		ASC
		,		a.ReportId	ASC

	IF @ReturnAuditInfo = 1
		BEGIN
	
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
	
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.ReportId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	

			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		
						a.ReportId	
					,	c.AuditActionId 
					,	a.CreatedDate
					,	a.CreatedByAuditId	
					, 	a.ModifiedDate
					,	a.ModifiedByAuditId			
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.ReportId
			INNER JOIN	CommonServices.dbo.AuditHistory						c
						ON	c.AuditHistoryId	= b.MaxAuditHistoryId
			INNER JOIN	CommonServices.dbo.AuditAction						d	
						ON	c.AuditActionId 	= d.AuditActionId
			INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
						ON	c.CreatedByPersonId	= e.ApplicationUserId

			-- Show full details
			SELECT		a.ReportId	
					,	a.ApplicationId	
					,	a.Application				
					,	a.Name
					,	a.Title						
					,	a.Description					
					,	a.SortOrder
					,	a.CreatedDate
					,	a.ModifiedDate
					,	a.CreatedByAuditId
					,	a.ModifiedByAuditId
					,	b.UpdatedDate   
					,	b.UpdatedBy
					,	b.LastAction
			FROM		#TempMain				a
			LEFT JOIN	#HistortyInfoDetails	b		
						ON	a.ReportId	= b.ReportId
			ORDER BY	a.SortOrder				ASC
					,	a.ReportId
		
		END

	ELSE
		BEGIN

			SELECT 	a.*
				, 	UpdatedDate = '1/1/1900'
				,	UpdatedBy	= 'Unknown'
				,	LastAction	= 'Unknown'
			FROM	#TempMain a		
			ORDER BY	a.SortOrder				ASC
					,	a.ReportId

		END

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Recorda
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'Report'
				,	@EntityKey				= @ReportId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END
END
GO

