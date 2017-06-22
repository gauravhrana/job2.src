IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='AboutPagesSearch')
BEGIN
	PRINT 'Dropping Procedure AboutPagesSearch'
	DROP Procedure AboutPagesSearch
END
GO

PRINT 'Creating Procedure AboutPagesSearch'
GO

/******************************************************************************
**		File: 
**		Name: AboutPagesSearch
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
			EXEC AboutPagesSearch NULL	, NULL	, NULL
			EXEC AboutPagesSearch NULL	, 'K'	, NULL
			EXEC AboutPagesSearch 1		, 'K'	, NULL
			EXEC AboutPagesSearch 1		, NULL	, NULL
			EXEC AboutPagesSearch NULL	, NULL	, 'W'

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

Create Procedure dbo.AboutPagesSearch
(
		@AboutPagesId			INT				= NULL	
	,	@ApplicationId			INT				= NULL
	,	@Description			VARCHAR (500)	= NULL
	,	@Developer				VARCHAR (100)	= NULL
	,	@JIRAId					VARCHAR (100)	= NULL
	,	@Feature				VARCHAR (100)	= NULL
	,	@PrimaryEntity			VARCHAR (100)	= NULL
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'AboutPages'
)	
AS
BEGIN

	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'AboutPagesId' + ', ' + 'JIRAId' + ', ' + '@Description'
	SET @InputValuesLocal			= CAST(@AboutPagesId AS VARCHAR(50)) + ', '+ ISNULL(@JIRAId, 'NULL') + ', '+ ISNULL(@Description, 'NULL') 

	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.AboutPagesSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal

	-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType,@ApplicationId)

  --if blank, then assume search on all possiblities ('%')
	IF  @JIRAId IS NULL OR LEN(RTRIM(LTRIM(@JIRAId))) = 0
	BEGIN
		SET	@JIRAId = '%'
	END

	--if blank, then assume search on all possiblities ('%')
	IF  @Description IS NULL OR LEN(RTRIM(LTRIM(@Description))) = 0
	BEGIN
		SET	@Description = '%'
	END
	
	SELECT		a.AboutPagesId
			,	a.ApplicationId		
			,	Description		
			,	Developer
			,	JIRAId
			,	Feature
			,	PrimaryEntity
	INTO		#TempMain		
	FROM		dbo.AboutPages a	
	WHERE		a.JIRAId		LIKE @JIRAId + '%'
	AND			a.Description	LIKE @Description + '%'
	AND			a.AboutPagesId		= ISNULL(@AboutPagesId, a.AboutPagesId)
	AND			a.ApplicationId			= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.JIRAId			ASC
		,		a.AboutPagesId	ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.AboutPagesId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.AboutPagesId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.AboutPagesId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.AboutPagesId	
			,	a.ApplicationId	
			,	Description		
			,	Developer
			,	JIRAId
			,	Feature
			,	PrimaryEntity	
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.AboutPagesId	= b.AboutPagesId
	ORDER BY	a.SortOrder				ASC
			,	a.AboutPagesId

	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @AboutPagesId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
