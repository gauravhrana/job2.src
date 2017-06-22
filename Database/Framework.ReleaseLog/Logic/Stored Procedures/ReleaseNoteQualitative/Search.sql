IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ReleaseNoteQualitativeSearch')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteQualitativeSearch'
	DROP Procedure ReleaseNoteQualitativeSearch
END
GO

PRINT 'Creating Procedure ReleaseNoteQualitativeSearch'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseNoteQualitativeSearch
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
			EXEC ReleaseNoteQualitativeSearch NULL , NULL	, NULL
			EXEC ReleaseNoteQualitativeSearch NULL , 'K'	, NULL
			EXEC ReleaseNoteQualitativeSearch 1	 , 'K'	, NULL
			EXEC ReleaseNoteQualitativeSearch 1	 , NULL	, NULL
			EXEC ReleaseNoteQualitativeSearch NULL	 , NULL	, 'W'

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
Create procedure dbo.ReleaseNoteQualitativeSearch
(
		@ReleaseNoteQualitativeId		INT				= NULL 
	,	@ApplicationId					INT			    = NULL						
	,	@Name							VARCHAR(50)		= NULL 
	,	@Description					VARCHAR(500)	= NULL 			
	,	@TimeOfExecution				VARCHAR(50)		= NULL			
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseNoteQualitative'
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
			SET @InputParametersLocal		= 'ReleaseNoteQualitativeId' + ', ' + 'Name' 
			SET @InputValuesLocal			=  CAST(@ReleaseNoteQualitativeId AS VARCHAR(50)) + ', ' + @Name 
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.ReleaseNoteQualitativeSearch'	
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

	SELECT	a.ReleaseNoteQualitativeId
		,	a.ApplicationId		
		,	a.Name			
		,	a.Description		
		,	a.SortOrder
		,	a.DateCreated
		,	a.DateModified
		,	a.CreatedByAuditId 
		,	a.ModifiedByAuditId
	INTO	#TempMain
	FROM	dbo.ReleaseNoteQualitative		a
	WHERE	a.Name				LIKE @Name	+ '%'
	AND		a.Description		LIKE @Description + '%'
	AND		a.ReleaseNoteQualitativeId			= ISNULL(@ReleaseNoteQualitativeId, a.ReleaseNoteQualitativeId)
	AND		a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.DateCreated			= ISNULL(@DateCreated, a.DateCreated)
	AND		a.DateModified			= ISNULL(@DateModified, a.DateModified)
	AND		a.CreatedByAuditId		= ISNULL(@CreatedByAuditId, a.CreatedByAuditId)
	AND		a.ModifiedByAuditId		= ISNULL(@ModifiedByAuditId, a.ModifiedByAuditId)
	ORDER BY	a.SortOrder						ASC
		,		a.Name							ASC
		,		a.ReleaseNoteQualitativeId	ASC

	IF @ReturnAuditInfo = 1
		BEGIN
			
			---- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.ReleaseNoteQualitativeId			AS 'ReleaseNoteQualitativeId'			
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
				
			SELECT 		a.ReleaseNoteQualitativeId
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
						ON	a.ReleaseNoteQualitativeId	= b.ReleaseNoteQualitativeId
			ORDER BY	a.SortOrder				ASC
					,	a.ReleaseNoteQualitativeId
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
					,	a.ReleaseNoteQualitativeId

		END

	IF @AddAuditInfo = 1 
		BEGIN

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
				@SystemEntityType		= 'ReleaseNoteQualitative'
			,	@EntityKey				= @ReleaseNoteQualitativeId
			,	@AuditAction			= 'Search'
			,	@CreatedDate			= @AuditDate
			,	@CreatedByPersonId		= @AuditId
		
		END

END
GO
	

