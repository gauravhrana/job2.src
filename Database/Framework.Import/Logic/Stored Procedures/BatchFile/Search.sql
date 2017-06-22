IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileSearch')
BEGIN
	PRINT 'Dropping Procedure BatchFileSearch'
	DROP  Procedure  BatchFileSearch
END
GO

PRINT 'Creating Procedure BatchFileSearch'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.BatchFileSearch
(
		@BatchFileId			INT				= NULL	
	,	@ApplicationId			INT				= NULL
	,	@Name					VARCHAR(50)		= NULL	
	,	@BatchFileSetId			INT				= NULL
	,	@FileTypeId				INT				= NULL	
	,	@SystemEntityId			INT				= NULL  
	,	@BatchFileStatusId		INT				= NULL  
	,	@PersonId				INT				= NULL
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'BatchFile'
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
-- TRACE AND LOGGING ---
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'Name' 
	SET @InputValuesLocal			= @Name
	EXEC LoggingAndTrace.dbo.StoredProcedureLogInsert
			@Name						= 'dbo.BatchFileSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
	END
	SET	@Name = ISNULL(@Name,'%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@Name))) = 0 
	BEGIN
		SET	@Name = '%'
	END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	SELECT	a.BatchFileId
		,	a.ApplicationId	 
		,	a.Name			 
		,	a.Folder
		,	a.BatchFile
		,	a.BatchFileSetId
		,	a.Description
		,	a.FileTypeId
		,	a.SystemEntityTypeId
		,	a.BatchFileStatusId
		,	a.CreatedDate
		,	a.CreatedByPersonId
		,	a.UpdatedDate
		,	a.UpdatedByPersonId
		,	a.Errors
		,	g.Name							AS 'BatchFileSet'
		,	c.EntityName					AS 'SystemEntityType'
		,	b.Name							AS 'FileType'
		,	f.Name							AS 'BatchFileStatus'
		,	d.FirstName + ' ' + d.LastName	AS 'CreatedByPerson'
		,	e.FirstName + ' ' + e.LastName	AS 'UpdatedByPerson'
	INTO	#TempMain
	FROM	dbo.BatchFile a
			INNER JOIN dbo.FileType							b		ON		a.FileTypeId			= b.FileTypeId
			INNER JOIN dbo.BatchFileStatus					f		ON		a.BatchFileStatusId		= f.BatchFileStatusId
			INNER JOIN Configuration.dbo.SystemEntityType	c		ON		a.SystemEntityTypeId	= c.SystemEntityTypeId
			INNER JOIN dbo.BatchFileSet						g		ON		a.BatchFileSetId		= g.BatchFileSetId
			INNER JOIN AuthenticationAndAuthorization.dbo.ApplicationUser			d		ON		a.CreatedByPersonId		= d.ApplicationUserId	
			INNER JOIN AuthenticationAndAuthorization.dbo.ApplicationUser			e		ON		a.UpdatedByPersonId		= e.ApplicationUserId	
	WHERE	a.Name LIKE @Name + '%'
	AND a.FileTypeId		 = ISNULL(@FileTypeId, a.FileTypeId )
	AND a.BatchFileStatusId	 = ISNULL(@BatchFileStatusId, a.BatchFileStatusId )
	AND a.SystemEntityTypeId = ISNULL(@SystemEntityTypeId, a.SystemEntityTypeId )
	AND a.BatchFileSetId	 = ISNULL(@BatchFileSetId, a.BatchFileSetId )
	AND a.BatchFileId		 = ISNULL(@BatchFileId, a.BatchFileId )
	AND a.ApplicationId		 = ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY	a.Name		ASC
			,	a.BatchFileId	ASC	
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE BatchFileId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.BatchFileId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.BatchFileId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.BatchFileId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId	
	
	SELECT 	a.BatchFileId
		,	a.ApplicationId	 
		,	a.Name			 
		,	a.Folder
		,	a.BatchFile
		,	a.BatchFileSetId
		,	a.Description
		,	a.FileTypeId
		,	a.SystemEntityTypeId
		,	a.BatchFileStatusId
		,	a.CreatedDate
		,	a.CreatedByPersonId
		,	a.UpdatedDate
		,	a.UpdatedByPersonId
		,	a.Errors
		,	a.BatchFileSet
		,	a.SystemEntityType
		,	a.FileType
		,	a.BatchFileStatus
		,	a.CreatedByPerson
		,	a.UpdatedByPerson			
		, 	a.UpdatedDate
		,	a.UpdatedBy
		,	a.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.BatchFileId	= b.BatchFileId
	ORDER BY	a.BatchFileId
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
		ORDER BY	a.BatchFileId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END		
GO