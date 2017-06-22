IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='BatchFileStatusSearch')
BEGIN
	PRINT 'Dropping Procedure BatchFileStatusSearch'
	DROP Procedure BatchFileStatusSearch
END
GO

PRINT 'Creating Procedure BatchFileStatusSearch'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileStatusSearch
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
			EXEC BatchFileStatusSearch NULL	, NULL	, NULL
			EXEC BatchFileStatusSearch NULL	, 'K'	, NULL
			EXEC BatchFileStatusSearch 1	, 'K'	, NULL
			EXEC BatchFileStatusSearch 1	, NULL	, NULL
			EXEC BatchFileStatusSearch NULL	, NULL	, 'W'

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
Create procedure BatchFileStatusSearch
(
		@BatchFileStatusId		INT				= NULL 	
	,	@ApplicationId			INT				= NULL
	,	@Name					VARCHAR(50)		= NULL 	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'BatchFileStatus'
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	

	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END

	SELECT	a.BatchFileStatusId
		,	a.Name
		,	a.Description
		,	a.SortOrder
		,	a.ApplicationId
	INTO	#TempMain
	FROM	dbo.BatchFileStatus a
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.ApplicationId		 = ISNULL(@ApplicationId, a.ApplicationId)
	AND a.BatchFileStatusId	 = ISNULL(@BatchFileStatusId, a.BatchFileStatusId)
	ORDER BY a.SortOrder			ASC
		,	 a.Name					ASC
		,	 a.BatchFileStatusId	ASC
		,	 a.ApplicationId		ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE BatchFileStatusId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.BatchFileStatusId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.BatchFileStatusId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.BatchFileStatusId
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
				ON	a.BatchFileStatusId	= b.BatchFileStatusId
	ORDER BY	a.SortOrder				ASC
			,	a.BatchFileStatusId
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
				,	a.BatchFileStatusId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileStatusId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO
	

