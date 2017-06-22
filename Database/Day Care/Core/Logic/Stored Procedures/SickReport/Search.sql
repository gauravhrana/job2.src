IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='SickReportSearch')
BEGIN
	PRINT 'Dropping Procedure SickReportSearch'
	DROP Procedure SickReportSearch
END
GO

PRINT 'Creating Procedure SickReportSearch'
GO

/******************************************************************************
**		File: 
**		StudentID: SickReportSearch
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
			EXEC SickReportSearch NULL	, NULL	, NULL
			EXEC SickReportSearch NULL	, 'K'	, NULL
			EXEC SickReportSearch 1		, 'K'	, NULL
			EXEC SickReportSearch 1		, NULL	, NULL
			EXEC SickReportSearch NULL	, NULL	, 'W'

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

Create Procedure dbo.SickReportSearch
(
	    @SickReportId       INT		    = NULL			 
	,	@ApplicationId		INT			= NULL	 
	,	@StudentId          INT			= NULL		 
	,	@TuitionId			INT			= NULL	 
	,	@TypeOfSickness     VARCHAR(50)	= NULL	 
	,	@AmountOfSickness   VARCHAR(50) = NULL   
	,	@FreqOfSickness     VARCHAR(50)	= NULL   
	,	@TeacherSickNote    VARCHAR(100)= NULL   
	,	@ReturnToSchoolDate DATETIME	= NULL	      
	,   @AuditId			INT					 	
	,   @AuditDate			DATETIME	= NULL
	,   @SystemEntityType   VARCHAR(50) = 'SickReport'
)	
AS
BEGIN
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'SickReportId' + ', ' + 'StudentId' 
	SET @InputValuesLocal			= CAST(@SickReportId As VARCHAR(50)) + ', ' + CAST(@StudentId As VARCHAR(50)) 
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.SickReportSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		,	@ExecutedBy					= @AuditId

		-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType,@ApplicationId)
			
	SELECT		a.*	
	INTO		#TempMain		
	FROM		dbo.SickReport a	
	WHERE		a.SickReportId		= ISNULL(@SickReportId, a.SickReportId)
	AND			a.StudentId			= ISNULL(@StudentId, a.StudentId)
	AND			a.ApplicationId		= ISNULL(@ApplicationId, a.ApplicationId)	
	ORDER BY	a.StudentId         ASC
		,		a.SickReportId		ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.SickReportId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.SickReportId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.SickReportId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.*	
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.SickReportId	= b.SickReportId
	ORDER BY	a.StudentId				ASC
			,	a.SickReportId      ASC
									
	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @SickReportId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByStudentId		= @AuditId
	
END
GO
