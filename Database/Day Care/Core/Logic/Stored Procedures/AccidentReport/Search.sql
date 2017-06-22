IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='AccidentReportSearch')
BEGIN
	PRINT 'Dropping Procedure AccidentReportSearch'
	DROP Procedure AccidentReportSearch
END
GO

PRINT 'Creating Procedure AccidentReportSearch'
GO

/******************************************************************************
**		File: 
**		Name: AccidentReportSearch
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
			EXEC AccidentReportSearch NULL	, NULL	, NULL
			EXEC AccidentReportSearch NULL	, 'K'	, NULL
			EXEC AccidentReportSearch 1		, 'K'	, NULL
			EXEC AccidentReportSearch 1		, NULL	, NULL
			EXEC AccidentReportSearch NULL	, NULL	, 'W'

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

Create Procedure dbo.AccidentReportSearch
(
	    @AccidentReportId		INT             = NULL
	,	@ApplicationId			INT				= NULL
	,	@StudentId				INT				= NULL
	,	@Date					DATETIME		= NULL
	,	@AccidentPlaceId		INT				= NULL
	,	@AccidentPlace			VARCHAR(50)		= NULL
	,	@TeacherId				INT				= NULL
	,	@Description			VARCHAR(500)	= NULL	
	,	@Remedy					VARCHAR(200)	= NULL
	,	@SignoffParent			BIT				
	,	@SignoffTeacher			BIT				= NULL
	,	@SignoffAdmin			BIT				= NULL
	,   @AuditId				INT					
	,   @AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'AccidentReport'		
)	
AS
BEGIN

	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'StudentId' + ', ' + 'AccidentReportId'
	SET @InputValuesLocal			= CAST(@StudentId As VARCHAR(50)) + ', ' +  CAST(@AccidentReportId As VARCHAR(50))
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.AccidentReportSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal

		-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType,@ApplicationId)
			
	SELECT		a.AccidentReportId
			,	a.ApplicationId		
			,	a.StudentId				
			,	a.Date		
			,	a.AccidentPlaceId
			,	a.TeacherId
			,   a.Description
			,	a.Remedy
			,	a.SignoffParent
			,	a.SignoffTeacher
			,	a.SignoffAdmin	
	INTO		#TempMain		
	FROM		dbo.AccidentReport a	
	WHERE		a.AccidentReportId		= ISNULL(@AccidentReportId, a.AccidentReportId)
	AND			a.StudentId				= ISNULL(@StudentId, a.StudentId)
	AND			a.ApplicationId			= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.StudentId         ASC
		,		a.AccidentReportId	ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.AccidentReportId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.AccidentReportId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.AccidentReportId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.AccidentReportId	
			,	a.ApplicationId	
			,	a.StudentId				
			,	a.Date		
			,	a.AccidentPlaceId
			,	a.TeacherId
			,   a.Description
			,	a.Remedy
			,	a.SignoffParent
			,	a.SignoffTeacher
			,	a.SignoffAdmin	
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.AccidentReportId	= b.AccidentReportId
	ORDER BY	a.StudentId				ASC
			,	a.AccidentReportId      ASC
									
	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @AccidentReportId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByStudentId		= @AuditId
	
END
Go
