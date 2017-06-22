IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserSearch'
	DROP  Procedure  ApplicationUserSearch
END
GO

PRINT 'Creating Procedure ApplicationUserSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserSearch
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
			EXEC ApplicationUserSearch NULL	, NULL	, NULL
			EXEC ApplicationUserSearch NULL	, 'K'	, NULL
			EXEC ApplicationUserSearch 1	, 'K'	, NULL
			EXEC ApplicationUserSearch 1	, NULL	, NULL
			EXEC ApplicationUserSearch NULL	, NULL	, 'W'

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
CREATE Procedure dbo.ApplicationUserSearch
(
		@ApplicationUserId			INT				=	NULL	
	,	@LastName					VARCHAR(50)		=	NULL	
	,	@FirstName					VARCHAR(50)		=	NULL	
	,	@MiddleName					VARCHAR(50)		=	NULL  
	,	@ApplicationId				INT
	,	@ApplicationUserTitleId		INT				=	NULL	
	,	@AuditId					INT							
	,	@AuditDate					DATETIME		=	NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ApplicationUser'	
)
AS
BEGIN
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'FirstName' + ', '+ 'LastName'
	SET @InputValuesLocal			= @FirstName + ', ' + @LastName
	EXEC dbo.StoredProcedureLogInsert
			@Name					= 'dbo.ApplicationUserSearch'
		,	@InputParameters		= @InputParametersLocal
		,	@InputValues			= @InputValuesLocal 

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId	AS INT
	EXEC	dbo.SystemEntityTypeGetId 
			@SystemEntityType	=	@SystemEntityType
		,	@SystemEntityTypeId =	@SystemEntityTypeId OUTPUT

	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET	@FirstName	= ISNULL(@FirstName, '%')					
	SET	@LastName	= ISNULL(@LastName, '%')
	SET	@MiddleName	= ISNULL(@MiddleName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@FirstName))) = 0 
	BEGIN
		SET	@FirstName = '%'
	END

	IF LEN(LTRIM(RTRIM(@LastName))) = 0 
	BEGIN
		SET	@LastName = '%'
	END

	IF LEN(LTRIM(RTRIM(@MiddleName))) = 0 
	BEGIN
		SET	@MiddleName = '%'
	END
	
	--Here only the first charecter of the string is required to search	
	SELECT	a.ApplicationUserId	
		,	a.ApplicationId										
		,	a.FirstName										
		,	a.LastName	
		,	a.MiddleName
		,	a.ApplicationUserTitleId
		,	b.Name													AS	'ApplicationUserTitle'								
		,	a.FirstName	+ ' ' + a.LastName +	' ' + a.LastName	AS	'FullName'
		,	c.Name													AS	'Application'			
	INTO		#TempMain
	FROM		dbo.ApplicationUser			a
	INNER JOIN  dbo.ApplicationUserTitle	b	ON	a.ApplicationUserTitleId	= b.ApplicationUserTitleId
	INNER JOIN	dbo.Application				c	ON	a.ApplicationId				= c.ApplicationId
	WHERE	a.LastName		LIKE @LastName	+ '%'
	AND		a.FirstName		LIKE @FirstName	+ '%'
	AND		a.MiddleName	LIKE @MiddleName + '%'
	AND a.ApplicationUserTitleId		  = ISNULL(@ApplicationUserTitleId,a.ApplicationUserTitleId )
	AND a.ApplicationId		  = ISNULL(@ApplicationId,a.ApplicationId )
	AND a.ApplicationUserID		  = ISNULL(@ApplicationUserID,a.ApplicationUserID )
	ORDER BY		a.LastName	ASC				 
				,	a.FirstName ASC				
				,   a.MiddleName ASC			
				,	a.ApplicationUserId  ASC

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ApplicationUserId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ApplicationUserId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ApplicationUserId
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
				ON	a.ApplicationUserId	= b.ApplicationUserId
	ORDER BY	a.ApplicationUserId

	--Create Audit Record@CreatedByPersonId
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType				= @SystemEntityType	 
		,	@EntityKey						= @ApplicationUserId
		,	@AuditAction					= 'Search' 
		,	@CreatedDate					= @AuditDate
		,	@CreatedByPersonId				= @AuditId


END
GO

