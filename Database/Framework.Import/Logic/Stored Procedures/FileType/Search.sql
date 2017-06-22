IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='FileTypeSearch')
BEGIN
	PRINT 'Dropping Procedure FileTypeSearch'
	DROP Procedure FileTypeSearch
END
GO

PRINT 'Creating Procedure FileTypeSearch'
GO

/******************************************************************************
**		File: 
**		Name: FileTypeSearch
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
			EXEC FileTypeSearch NULL	, NULL	, NULL
			EXEC FileTypeSearch NULL	, 'K'	, NULL
			EXEC FileTypeSearch 1		, 'K'	, NULL
			EXEC FileTypeSearch 1		, NULL	, NULL
			EXEC FileTypeSearch NULL	, NULL	, 'W'

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
Create procedure FileTypeSearch
(
		@FileTypeId				INT				= NULL 	
	,	@ApplicationId			INT				= NULL		
	,	@Name					VARCHAR(50)		= NULL 			
	,	@AuditId				INT								
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'FileType'
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

	SELECT	a.*
	INTO	#TempMain
	FROM	dbo.FileType a
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.ApplicationId	= ISNULL(@ApplicationId, a.ApplicationId)
	AND a.FileTypeId	= ISNULL(@FileTypeId, a.FileTypeId)
	ORDER BY a.SortOrder	ASC
		,	 a.Name			ASC
		,	 a.FileTypeId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE FileTypeId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.FileTypeId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.FileTypeId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.FileTypeId
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
				ON	a.FileTypeId	= b.FileTypeId
	ORDER BY	a.SortOrder				ASC
			,	a.FileTypeId
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
				,	a.FileTypeId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FileTypeId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO
	

