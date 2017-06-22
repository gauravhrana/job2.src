IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='DevelopmentCategorySearch')
BEGIN
	PRINT 'Dropping Procedure DevelopmentCategorySearch'
	DROP Procedure DevelopmentCategorySearch
END
GO

PRINT 'Creating Procedure DevelopmentCategorySearch'
GO

/******************************************************************************
**		File: 
**		Name: DevelopmentCategorySearch
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
			EXEC DevelopmentCategorySearch NULL , NULL	, NULL
			EXEC DevelopmentCategorySearch NULL , 'K'	, NULL
			EXEC DevelopmentCategorySearch 1	 , 'K'	, NULL
			EXEC DevelopmentCategorySearch 1	 , NULL	, NULL
			EXEC DevelopmentCategorySearch NULL	 , NULL	, 'W'

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
Create procedure dbo.DevelopmentCategorySearch
(
		@DevelopmentCategoryId		INT				= NULL 
	,	@ApplicationId					INT			    = NULL						
	,	@Name							VARCHAR(50)		= NULL 
	,	@Description					VARCHAR(500)	= NULL 			
	,	@TimeOfExecution				VARCHAR(50)		= NULL			
	,	@SystemEntityType				VARCHAR(50)		= 'DevelopmentCategory'
	,	@DateModified					DATETIME		= NULL
	,	@DateCreated					DATETIME		= NULL
	,	@CreatedByAuditId				INT				= NULL
	,	@ModifiedByAuditId				INT				= NULL				
	,	@AuditId						INT								
	,	@AuditDate						DATETIME		= NULL		
	,	@ApplicationMode				INT				= NULL		
	,	@AddAuditInfo					INT				 = 1
	,	@AddTraceInfo					INT				 = 0
	,	@ReturnAuditInfo				INT				 = 0		
)
WITH RECOMPILE
AS 
BEGIN	

	SET	NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN

			-- TRACE
			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'DevelopmentCategoryId' + ', ' + 'Name' 
			SET @InputValuesLocal			=  CAST(@DevelopmentCategoryId AS VARCHAR(50)) + ', ' + @Name 
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.DevelopmentCategorySearch'	
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal
			
			-- TRACE
		
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

	SELECT	a.DevelopmentCategoryId
		,	a.ApplicationId		
		,	a.Name			
		,	a.Description		
		,	a.SortOrder
		,	a.DateCreated
		,	a.DateModified
		,	a.CreatedByAuditId 
		,	a.ModifiedByAuditId
	INTO	#TempMain
	FROM	dbo.DevelopmentCategory		a
	WHERE	a.Name				LIKE @Name	+ '%'
	AND		a.Description		LIKE @Description + '%'
	AND		a.DevelopmentCategoryId			= ISNULL(@DevelopmentCategoryId, a.DevelopmentCategoryId)
	AND		a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.DateCreated			= ISNULL(@DateCreated, a.DateCreated)
	AND		a.DateModified			= ISNULL(@DateModified, a.DateModified)
	AND		a.CreatedByAuditId		= ISNULL(@CreatedByAuditId, a.CreatedByAuditId)
	AND		a.ModifiedByAuditId		= ISNULL(@ModifiedByAuditId, a.ModifiedByAuditId)
	ORDER BY	a.SortOrder						ASC
		,		a.Name							ASC
		,		a.DevelopmentCategoryId	ASC

	IF @ReturnAuditInfo = 1
		BEGIN
			
			---- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.DevelopmentCategoryId			AS 'DevelopmentCategoryId'			
					,	a.DateCreated
					,	a.CreatedByAuditId	
					, 	a.DateModified						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					, 'LastAction' = case WHEN (a.DateCreated = a.DateModified)
					   then 'INSERT'        --this is unnecessary
					   else 'UPDATE'	
				end				
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
						ON	a.CreatedByAuditId	= e.ApplicationUserId
				
			SELECT 		a.DevelopmentCategoryId
					,	a.ApplicationId		
					,	a.Name			
					,	a.Description		
					,	a.SortOrder
					,	a.DateCreated
					,	a.DateModified
					,	a.CreatedByAuditId
					,	a.ModifiedByAuditId
					,	b.UpdatedDate AS LastUpdatedDate
					,	b.UpdatedBy AS LastUpdatedBy
					,	b.LastAction AS LastAuditAction
			FROM		#TempMain				a
			LEFT JOIN	#HistortyInfoDetails	b	
						ON	a.DevelopmentCategoryId	= b.DevelopmentCategoryId
			ORDER BY	a.SortOrder				ASC
					,	a.DevelopmentCategoryId
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
					,	a.DevelopmentCategoryId

		END

	IF @AddAuditInfo = 1 
		BEGIN

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
				@SystemEntityType		= 'DevelopmentCategory'
			,	@EntityKey				= @DevelopmentCategoryId
			,	@AuditAction			= 'Search'
			,	@CreatedDate			= @AuditDate
			,	@CreatedByPersonId		= @AuditId
		
		END

END
GO
	

