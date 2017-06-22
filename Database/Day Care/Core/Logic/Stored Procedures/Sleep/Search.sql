IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='SleepSearch')
BEGIN
	PRINT 'Dropping Procedure SleepSearch'
	DROP Procedure SleepSearch
END
GO

PRINT 'Creating Procedure SleepSearch'
GO

/******************************************************************************
**		File: 
**		StudentID: SleepSearch
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
			EXEC SleepSearch NULL	, NULL	, NULL
			EXEC SleepSearch NULL	, 'K'	, NULL
			EXEC SleepSearch 1		, 'K'	, NULL
			EXEC SleepSearch 1		, NULL	, NULL
			EXEC SleepSearch NULL	, NULL	, 'W'

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

Create Procedure dbo.SleepSearch
(
		@SleepId			INT			= NULL				
	,	@ApplicationId		INT			= NULL			
	,	@StudentId			INT			= NULL					
	,	@Date				DATETIME	= NULL
	,	@NapStart			DATETIME	= NULL	
	,	@NapEnd			    DATETIME	= NULL			
	,	@AuditId			INT					        	
	,	@AuditDate			DATETIME	= NULL
	,   @SystemEntityType	VARCHAR(50) = 'Sleep'
)	
AS
BEGIN
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'SleepId' + ', ' + 'StudentId' 
	SET @InputValuesLocal			= CAST(@SleepId As VARCHAR(50)) + ', ' + CAST(@StudentId As VARCHAR(50)) 
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.SleepSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		,	@ExecutedBy					= @AuditId
	
		-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType,@ApplicationId)
			
	SELECT		a.SleepId	
			,	a.ApplicationId	
			,	a.StudentId				
			,	a.Date		
			,	a.NapStart
			,	a.NapEnd	
	INTO		#TempMain		
	FROM		dbo.Sleep a	
	WHERE		a.SleepId			= ISNULL(@SleepId, a.SleepId)
	AND			a.StudentId			= ISNULL(@StudentId, a.StudentId)
	AND			a.ApplicationId		= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.StudentId         ASC
		,		a.SleepId			ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.SleepId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.SleepId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.SleepId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.SleepId	
			,	a.ApplicationId	
			,	a.StudentId				
			,	a.Date		
			,	a.NapStart
			,	a.NapEnd	
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.SleepId	= b.SleepId
	ORDER BY	a.StudentId				ASC
			,	a.SleepId      ASC
									
	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert		
				@SystemEntityType		= @SystemEntityType	
			,	@EntityKey				= @SleepId
			,	@AuditAction			= 'Search' 
			,	@CreatedDate			= @AuditDate
			,	@CreatedByPersonId		= @AuditId	

END
GO
