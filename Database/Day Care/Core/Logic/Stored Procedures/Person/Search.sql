IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonSearch')
BEGIN
	PRINT 'Dropping Procedure PersonSearch'
	DROP  Procedure  PersonSearch
END
GO

PRINT 'Creating Procedure PersonSearch'
GO

/******************************************************************************
**		File: 
**		Name: PersonSearch
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
			EXEC PersonSearch NULL	, NULL	, NULL
			EXEC PersonSearch NULL	, 'K'	, NULL
			EXEC PersonSearch 1		, 'K'	, NULL
			EXEC PersonSearch 1		, NULL	, NULL
			EXEC PersonSearch NULL	, NULL	, 'W'

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
CREATE Procedure dbo.PersonSearch
(
		@PersonId				INT			= NULL	
	,	@ApplicationId			INT         = NULL	
	,	@LastName				VARCHAR(50)	= NULL	
	,	@FirstName				VARCHAR(50)	= NULL	
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'Person'	
)
AS
BEGIN
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'PersonId' + ', ' + 'LastName' + ', ' + 'FirstName' 
	SET @InputValuesLocal			= CAST(@PersonId As VARCHAR(50)) + ', ' + @LastName + ', ' + @FirstName
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.PersonSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType,@ApplicationId)

  --if blank, then assume search on all possiblities ('%')
	IF  @LastName IS NULL OR LEN(RTRIM(LTRIM(@LastName))) = 0
	BEGIN
		SET	@LastName = '%'
	END

	--if blank, then assume search on all possiblities ('%')
	IF  @FirstName IS NULL OR LEN(RTRIM(LTRIM(@FirstName))) = 0
	BEGIN
		SET	@FirstName = '%'
	END
	
	SELECT		a.PersonId
			,	a.ApplicationId		
			,	a.LastName				
			,	a.FirstName
			INTO		#TempMain		
	FROM		dbo.Person a	
	WHERE		a.LastName		LIKE @LastName  + '%'
	AND			a.FirstName 	LIKE @FirstName + '%'
	AND			a.PersonId		= ISNULL(@PersonId, a.PersonId)
	AND			a.ApplicationId = ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.LastName	ASC
		,		a.FirstName	ASC
		,		a.PersonId	ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.PersonId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.PersonId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.PersonId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.PersonId	
			,	a.ApplicationId	
			,	a.LastName			
			,	a.FirstName	
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
			ON	a.PersonId	= b.PersonId
	ORDER BY	a.LastName				ASC
			,	a.FirstName				ASC
			,	a.PersonId

	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert
			@SystemEntityType		= 'Person'	 
		,	@EntityKey				= @PersonId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByStudentId		= @AuditId
END
GO
