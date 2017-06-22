IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='NeedsSearch')
BEGIN
	PRINT 'Dropping Procedure NeedsSearch'
	DROP Procedure NeedsSearch
END
GO

PRINT 'Creating Procedure NeedsSearch'
GO

/******************************************************************************
**		File: 
**		Name: NeedsSearch
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
			EXEC NeedsSearch NULL	, NULL	, NULL
			EXEC NeedsSearch NULL	, 'K'	, NULL
			EXEC NeedsSearch 1		, 'K'	, NULL
			EXEC NeedsSearch 1		, NULL	, NULL
			EXEC NeedsSearch NULL	, NULL	, 'W'

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

Create Procedure dbo.NeedsSearch
(
	    @NeedsId		    INT         = NULL
	,	@ApplicationId		INT			= NULL				
	,	@StudentId			INT         = NULL
	,	@RequestDate		DATETIME	= NULL
	,   @ReceivedDate		DATETIME	= NULL
	,	@NeedItemId		    INT			= NULL
	,	@NeedItemStatus		VARCHAR(50)	= NULL
	,	@NeedItemBy			DATETIME	= NULL
	,   @AuditId			INT		
	,   @AuditDate			DATETIME	= NULL
	,   @SystemEntityType   VARCHAR(50) = 'Needs'
)	
AS
BEGIN
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'NeedsId' + ', ' + 'StudentId'  
	SET @InputValuesLocal			= CAST(@NeedsId As VARCHAR(50)) + ', ' + CAST(@StudentId As VARCHAR(50)) 
	EXEC dbo.StoredProcedureLogInsert
			@StoredProcedureLogId		= @StoredProcedureLogId	OUTPUT
		,	@Name						= 'dbo.NeedsSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		,	@ExecutedBy					= @AuditId

	-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType,@ApplicationId)
			
	SELECT		a.*	
	INTO		#TempMain		
	FROM		dbo.Needs a	
	WHERE		a.NeedsId			= ISNULL(@NeedsId, a.NeedsId)
	AND			a.StudentId			= ISNULL(@StudentId, a.StudentId)
	AND			a.ApplicationId		= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.StudentId     ASC
		,		a.NeedsId		ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.NeedsId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.NeedsId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.NeedsId
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
				ON	a.NeedsId	= b.NeedsId
	ORDER BY	a.StudentId				ASC
			,	a.NeedsId      ASC
									
	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @NeedsId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
