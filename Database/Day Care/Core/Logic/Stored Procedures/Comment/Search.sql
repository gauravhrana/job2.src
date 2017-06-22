IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='CommentSearch')
BEGIN
	PRINT 'Dropping Procedure CommentSearch'
	DROP Procedure CommentSearch
END
GO

PRINT 'Creating Procedure CommentSearch'
GO

/******************************************************************************
**		File: 
**		Name: CommentSearch
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
			EXEC CommentSearch NULL	, NULL	, NULL
			EXEC CommentSearch NULL	, 'K'	, NULL
			EXEC CommentSearch 1	, 'K'	, NULL
			EXEC CommentSearch 1	, NULL	, NULL
			EXEC CommentSearch NULL	, NULL	, 'W'

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

Create Procedure dbo.CommentSearch
(
	    @CommentId		    INT             = NULL
	,	@ApplicationId		INT				= NULL
	,	@StudentId			INT             = NULL
	,	@Date				TIMESTAMP		= NULL
	,	@EventTypeId		INT				= NULL
	,	@Comment			VARCHAR(500)	= NULL
	,   @AuditId			INT		
	,   @AuditDate			DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'Comment'	
)	
AS
BEGIN
	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId INT
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'CommentId' + ', ' + 'StudentId' 
	SET @InputValuesLocal			= CAST(@CommentId As VARCHAR(50)) + ', ' + CAST(@StudentId As VARCHAR(50))
	EXEC dbo.StoredProcedureLogInsert
	     	@Name						= 'dbo.CommentSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		,	@ExecutedBy					= @AuditId	

	-- TRACE --	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType,@ApplicationId)
			
	SELECT		a.CommentId
			,	a.ApplicationId		
			,	a.StudentId				
			,	a.Date	
			,	a.EventTypeId	
			,	a.Comment
	INTO		#TempMain		
	FROM		dbo.Comment a	
	WHERE		a.CommentId		    = ISNULL(@CommentId, a.CommentId)
	AND			a.StudentId			= ISNULL(@StudentId, a.StudentId)
	AND			a.ApplicationId		= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.StudentId         ASC
		,		a.CommentId	ASC
			
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.CommentId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey			
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		
				a.CommentId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.CommentId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId
	
	-- Show full details
	SELECT 		a.CommentId
			,	a.ApplicationId		
			,	a.StudentId				
			,	a.Date	
			,	a.EventTypeId	
			,	a.Comment
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.CommentId	= b.CommentId
	ORDER BY	a.StudentId		  ASC
			,	a.CommentId      ASC
									
	--Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @CommentId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO