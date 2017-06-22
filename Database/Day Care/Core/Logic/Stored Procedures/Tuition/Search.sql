IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='TuitionSearch')
BEGIN
	PRINT 'Dropping Procedure TuitionSearch'
	DROP Procedure TuitionSearch
END
GO

PRINT 'Creating Procedure TuitionSearch'
GO

/******************************************************************************
**		File: 
**		Name: TuitionSearch
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
			EXEC TuitionSearch NULL	, NULL	, NULL
			EXEC TuitionSearch NULL	, 'K'	, NULL
			EXEC TuitionSearch 1	, 'K'	, NULL
			EXEC TuitionSearch 1	, NULL	, NULL
			EXEC TuitionSearch NULL	, NULL	, 'W'

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
Create Procedure dbo.TuitionSearch
(
		@TuitionId               INT	  = NULL			
	,	@ApplicationId			 INT	  = NULL	 
	,	@StudentId               INT	  = NULL			
	,	@TuitionDueDate			 DATETIME = NULL		
	,	@TuitionAmount			 FLOAT    = NULL
	,	@DiscountId              INT	  = NULL			
	,	@DiscountAmount          FLOAT    = NULL		
	,	@TuitionAmountDue		 FLOAT    = NULL
	,	@PaymentMethodId         INT	  = NULL			
	,	@TuitionPaymentAmount	 FLOAT    = NULL		
	,	@AuditId				 INT					
	,   @AuditDate				 DATETIME	= NULL
	,   @SystemEntityType		 VARCHAR(50) = 'Tuition'
)	
AS
BEGIN
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'TuitionId' + ', ' + 'StudentId'  
	SET @InputValuesLocal			= CAST(@TuitionId As VARCHAR(50)) + ', ' + CAST(@StudentId As VARCHAR(50)) 
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.TuitionSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		,	@ExecutedBy					= @AuditId
	
		-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType,@ApplicationId)
			
	SELECT		a.*	
	INTO		#TempMain		
	FROM		dbo.Tuition a	
	WHERE		a.TuitionId			= ISNULL(@TuitionId, a.TuitionId)
	AND			a.StudentId			= ISNULL(@StudentId, a.StudentId)
	AND			a.ApplicationId		= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.StudentId         ASC
		,		a.TuitionId			ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.TuitionId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.TuitionId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.TuitionId
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
				ON	a.TuitionId	= b.TuitionId
	ORDER BY	a.StudentId		 ASC
			,	a.TuitionId      ASC
									
	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @TuitionId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByStudentId		= @AuditId
	
END
GO
