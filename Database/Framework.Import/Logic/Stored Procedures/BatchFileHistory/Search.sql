IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='BatchFileHistorySearch')
BEGIN
	PRINT 'Dropping Procedure BatchFileHistorySearch'
	DROP Procedure BatchFileHistorySearch
END
GO

PRINT 'Creating Procedure BatchFileHistorySearch'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileHistorySearch
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
			EXEC BatchFileHistorySearch NULL	, NULL	, NULL
			EXEC BatchFileHistorySearch NULL	, 'K'	, NULL
			EXEC BatchFileHistorySearch 1		, 'K'	, NULL
			EXEC BatchFileHistorySearch 1		, NULL	, NULL
			EXEC BatchFileHistorySearch NULL	, NULL	, 'W'

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
Create procedure BatchFileHistorySearch
(
		@BatchFileHistoryId		INT				= NULL 	
	,	@BatchFileSetId			INT				= NULL 	
	,	@BatchFileStatusId		INT				= NULL 	
	,	@BatchFileId			INT				= NULL 	
	,	@PersonId				INT				= NULL 
	,	@ApplicationId			INT	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'BatchFileHistory'
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
	
	SELECT	a.BatchFileHistoryId
		,	a.ApplicationId									
		,	a.BatchFileId											
		,	a.BatchFileSetId										
		,	a.BatchFileStatusId										
		,	a.UpdatedDate											
		,	a.UpdatedByPersonId										
		,	b.Name							AS	'BatchFileSet'						
		,	c.Name							AS	'BatchFileStatus'					
		,	d.FirstName + ' ' + d.LastName	AS 'UpdatedByPerson'
	INTO		#TempMain
	FROM		dbo.BatchFileHistory	a
	INNER JOIN	dbo.BatchFileSet	 b ON a.BatchFileSetId	= b.BatchFileSetId
	INNER JOIN	dbo.BatchFileStatus	 c ON a.BatchFileStatusId	= c.BatchFileStatusId	
	-- Person is not Right solution is outdated. ApplicationUser is lastest
	--INNER JOIN	TaskTimeTracker.dbo.Person				d		ON		a.UpdatedByPersonId		= d.PersonId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	d		ON		a.UpdatedByPersonId		= d.ApplicationUserId
	WHERE	a.ApplicationId	  = ISNULL(@ApplicationId, a.ApplicationId)
	AND a.BatchFileId		  = ISNULL(@BatchFileId, a.BatchFileId) 
	AND a.BatchFileStatusId	  = ISNULL(@BatchFileStatusId, a.BatchFileStatusId )
	AND a.UpdatedByPersonId	  = ISNULL(@UpdatedByPersonId, a.UpdatedByPersonId )
	AND a.BatchFileHistoryId  = ISNULL(@BatchFileHistoryId, a.BatchFileHistoryId) 
	ORDER BY a.BatchFileHistoryId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE BatchFileHistoryId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.BatchFileHistoryId
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
				ON	b.EntityKey			= a.BatchFileHistoryId
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
				ON	a.BatchFileHistoryId	= b.BatchFileHistoryId
	ORDER BY	a.BatchFileHistoryId
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
		-- No Column Named as SortOrder in  #TempMain
		--ORDER BY	a.SortOrder				ASC
		ORDER BY	a.BatchFileHistoryId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileHistoryId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
END
GO
	

