IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationUserTitleSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleSearch'
	DROP Procedure ApplicationUserTitleSearch
END
GO

PRINT 'Creating Procedure ApplicationUserTitleSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserTitleSearch
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
			EXEC ApplicationUserTitleSearch NULL	, NULL	, NULL
			EXEC ApplicationUserTitleSearch NULL	, 'K'	, NULL
			EXEC ApplicationUserTitleSearch 1	, 'K'	, NULL
			EXEC ApplicationUserTitleSearch 1	, NULL	, NULL
			EXEC ApplicationUserTitleSearch NULL	, NULL	, 'W'

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
Create procedure dbo.ApplicationUserTitleSearch
(
		@ApplicationUserTitleId		INT				= NULL 	
	,	@ApplicationId				INT				= NULL
	,	@Name						VARCHAR(50)		= NULL 	
	,	@AuditId					INT						
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'ApplicationUserTitle'
)
AS
BEGIN

	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'Name' 
	SET @InputValuesLocal			= @Name
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.ApplicationUserTitleSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId	AS INT
	EXEC	dbo.SystemEntityTypeGetId 
			@SystemEntityType	=	@SystemEntityType
		,	@SystemEntityTypeId =	@SystemEntityTypeId OUTPUT


	-- if the ApplicationUserTitle did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END

	SELECT	a.*
	INTO	#TempMain
	FROM	dbo.ApplicationUserTitle a
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.ApplicationId	= ISNULL(@ApplicationId,a.ApplicationId )
	AND a.ApplicationUserTitleId = ISNULL(@ApplicationUserTitleId,a.ApplicationUserTitleId )
	ORDER BY a.SortOrder	ASC
		,	 a.Name			ASC
		,	 a.ApplicationUserTitleId	ASC

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ApplicationUserTitleId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ApplicationUserTitleId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ApplicationUserTitleId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
				
	SELECT 	a.ApplicationUserTitleId
		,	a.ApplicationId			
		,	a.Name			
		,	a.Description		
		,	a.SortOrder			
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.ApplicationUserTitleId	= b.ApplicationUserTitleId
	ORDER BY	a.SortOrder				ASC
			,	a.ApplicationUserTitleId	


	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserTitleId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
	

