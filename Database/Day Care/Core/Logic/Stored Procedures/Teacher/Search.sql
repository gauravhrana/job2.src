IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TeacherSearch')
BEGIN
	PRINT 'Dropping Procedure TeacherSearch'
	DROP  Procedure  TeacherSearch
END
GO

PRINT 'Creating Procedure TeacherSearch'
GO

/******************************************************************************
**		File: 
**		Name: TeacherSearch
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
			EXEC TeacherSearch NULL	, NULL	, NULL
			EXEC TeacherSearch NULL	, 'K'	, NULL
			EXEC TeacherSearch 1	, 'K'	, NULL
			EXEC TeacherSearch 1	, NULL	, NULL
			EXEC TeacherSearch NULL	, NULL	, 'W'

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
CREATE Procedure dbo.TeacherSearch
(
		@TeacherId				INT			= NULL	
	,	@ApplicationId			INT			= NULL	
	,	@LastName				VARCHAR(50)	= NULL	
	,	@FirstName				VARCHAR(50)	= NULL	
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'Teacher'
)
AS
BEGIN
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'TeacherId' + ', ' + 'LastName' + ', ' + 'FirstName' 
	SET @InputValuesLocal			= CAST(@TeacherId As VARCHAR(50)) + ', ' + @LastName + ', ' + @FirstName
	EXEC dbo.StoredProcedureLogInsert
			@StoredProcedureLogId		= @StoredProcedureLogId	OUTPUT
		,	@Name						= 'dbo.TeacherSearch'	
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
	
	SELECT		a.TeacherId
			,	a.ApplicationId		
			,	a.LastName				
			,	a.FirstName
			INTO		#TempMain		
	FROM		dbo.Teacher a	
	WHERE		a.LastName		LIKE @LastName  + '%'
	AND			a.FirstName 	LIKE @FirstName + '%'
	AND			a.TeacherId		= ISNULL(@TeacherId, a.TeacherId)
	AND			a.ApplicationId = ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.LastName	ASC
		,		a.FirstName	ASC
		,		a.TeacherId	ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.TeacherId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.TeacherId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.TeacherId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.TeacherId	
			,	a.ApplicationId	
			,	a.LastName			
			,	a.FirstName	
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
			ON	a.TeacherId	= b.TeacherId
	ORDER BY	a.LastName				ASC
			,	a.FirstName				ASC
			,	a.TeacherId

	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @TeacherId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByTeacherId		= @AuditId
END
GO
