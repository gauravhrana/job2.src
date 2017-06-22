IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='BatchFileSetSearch')
BEGIN
	PRINT 'Dropping Procedure BatchFileSetSearch'
	DROP Procedure BatchFileSetSearch
END
GO

PRINT 'Creating Procedure BatchFileSetSearch'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileSetSearch
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
			EXEC BatchFileSetSearch NULL	, NULL	, NULL
			EXEC BatchFileSetSearch NULL	, 'K'	, NULL
			EXEC BatchFileSetSearch 1		, 'K'	, NULL
			EXEC BatchFileSetSearch 1		, NULL	, NULL
			EXEC BatchFileSetSearch NULL	, NULL	, 'W'

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
Create procedure BatchFileSetSearch
(
		@BatchFileSetId			INT				= NULL	
	,	@ApplicationId			INT		 		= NULL 
	,	@Name					VARCHAR(50)		= NULL 	
	,	@CreatedByPersonId		INT				= NULL 	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'BatchFileSet'
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	
	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END	

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
		
	SELECT	a.BatchFileSetId
		,	a.ApplicationId		
		,	a.Name						
		,	a.Description			
		,	a.CreatedDate			
		,	a.CreatedByPersonId		
		,	b.FirstName + ' ' + b.LastName	AS 'CreatedByPerson'
	INTO	#TempMain
	FROM	dbo.BatchFileSet a
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	b	ON	a.CreatedByPersonId	= b.ApplicationUserId
	WHERE		a.Name LIKE @Name	+ '%'
	AND a.CreatedByPersonId	  = ISNULL(@CreatedByPersonId, a.CreatedByPersonId)
	AND a.ApplicationId		  = ISNULL(@ApplicationId, a.ApplicationId)
	AND a.BatchFileSetId	  = ISNULL(@BatchFileSetId, a.BatchFileSetId)
	ORDER BY a.Name		ASC,
			 a.BatchFileSetId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE BatchFileSetId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.BatchFileSetId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.BatchFileSetId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.BatchFileSetId
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
				ON	a.BatchFileSetId	= b.BatchFileSetId
	ORDER BY	a.BatchFileSetId
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
		ORDER BY	a.BatchFileSetId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileSetId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
END
GO
	

