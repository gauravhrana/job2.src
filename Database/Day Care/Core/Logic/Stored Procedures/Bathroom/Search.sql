IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='BathroomSearch')
BEGIN
	PRINT 'Dropping Procedure BathroomSearch'
	DROP Procedure BathroomSearch
END
GO

PRINT 'Creating Procedure BathroomSearch'
GO

/******************************************************************************
**		File: 
**		Name: BathroomSearch
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
			EXEC BathroomSearch NULL	, NULL	, NULL
			EXEC BathroomSearch NULL	, 'K'	, NULL
			EXEC BathroomSearch 1		, 'K'	, NULL
			EXEC BathroomSearch 1		, NULL	, NULL
			EXEC BathroomSearch NULL	, NULL	, 'W'

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

Create Procedure dbo.BathroomSearch
(
	    @BathroomId		    INT				= NULL
	,	@ApplicationId		INT				= NULL
	,	@StudentId			INT				= NULL
	,	@TimeIn				DATETIME		= NULL
	,	@DiaperStatusId		INT				= NULL
	,	@DiaperCream		VARCHAR(50)		= NULL
	,	@PottyStatus		VARCHAR(50)		= NULL
	,	@TeacherId			INT				= NULL
	,	@TeacherNotes	    VARCHAR(50)		= NULL
	,   @AuditId			INT				
	,   @AuditDate			DATETIME		= NULL	
	,	@SystemEntityType	VARCHAR(50)   	= 'Bathroom'	
)	
AS
BEGIN
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'BathroomId' + ', ' + 'StudentId' 
	SET @InputValuesLocal			= CAST(@BathroomId As VARCHAR(50)) + ', ' + CAST(@StudentId As VARCHAR(50))
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.BathroomSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		,	@ExecutedBy					= @AuditId	

	-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType, @ApplicationId)
			
	SELECT		a.BathroomId
			,	a.ApplicationId		
			,	a.StudentId				
			,	a.TimeIn		
			,	a.DiaperStatusId
			,	a.DiaperCream
			,	a.PottyStatus
			,   a.TeacherId
			,	a.TeacherNotes
	INTO		#TempMain		
	FROM		dbo.Bathroom a	
	WHERE		a.BathroomId		= ISNULL(@BathroomId, a.BathroomId)
	AND			a.StudentId			= ISNULL(@StudentId, a.StudentId)
	AND			a.ApplicationId		= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.StudentId         ASC
		,		a.BathroomId		ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.BathroomId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.BathroomId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.BathroomId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.BathroomId	
			,	a.ApplicationId	
			,	a.StudentId				
			,	a.TimeIn		
			,	a.DiaperStatusId
			,	a.DiaperCream
			,	a.PottyStatus
			,   a.TeacherId
			,	a.TeacherNotes
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.BathroomId	= b.BathroomId
	ORDER BY	a.StudentId			 ASC
			,	a.BathroomId         ASC
									
	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert				
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @BathroomId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
