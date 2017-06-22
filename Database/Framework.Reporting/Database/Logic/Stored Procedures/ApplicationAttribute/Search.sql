IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationAttributeSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationAttributeSearch'
	DROP Procedure ApplicationAttributeSearch
END
GO

PRINT 'Creating Procedure ApplicationAttributeSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationAttributeSearch
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
			EXEC ApplicationAttributeSearch NULL	, NULL	, NULL
			EXEC ApplicationAttributeSearch NULL	, 'K'	, NULL
			EXEC ApplicationAttributeSearch 1		, 'K'	, NULL
			EXEC ApplicationAttributeSearch 1		, NULL	, NULL
			EXEC ApplicationAttributeSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure ApplicationAttributeSearch
(
		@ApplicationId						INT				= NULL			
	,	@RenderApplicationFilter			INT				= NULL
	,	@AuditId							INT								
	,	@AuditDate							DATETIME		= NULL			
	,	@SystemEntityType					VARCHAR(50)		= 'ApplicationAttribute'
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON

	IF @AddTraceInfo = 1 
	BEGIN

	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'ApplicationId' 
	SET @InputValuesLocal			=  @ApplicationId  
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.ApplicationAttributeSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'
	END		
	
	-- TRACE	
	
	SELECT	a.ApplicationId	
		,	a.RenderApplicationFilter	
		,	b.Name						AS 'Application'
	INTO	#TempMain
	FROM	dbo.ApplicationAttribute a	INNER JOIN Application b on a.ApplicationId = b.ApplicationId
	WHERE	a.RenderApplicationFilter			= ISNULL(@RenderApplicationFilter, a.RenderApplicationFilter )
	AND a.ApplicationId		= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.ApplicationId	ASC	
	
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ApplicationId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN


			 -- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ApplicationId
				AND c.SystemEntityId	= @ApplicationId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ApplicationId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ApplicationId
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
				ON	a.ApplicationId	= b.ApplicationId
	ORDER BY	a.ApplicationId
	 END
	ELSE
	BEGIN
		DECLARE @StaticUpdatedDate AS DATETIME
		SET @StaticUpdatedDate = Convert(datetime, '1/1/1900', 103)
	
		SELECT 	a.*
		   	,	UpdatedDate = @StaticUpdatedDate
			,	UpdatedBy	= 'Unknown'
			,	LastAction	= 'Unknown'
		FROM	#TempMain a		
		ORDER BY		a.ApplicationId
	END		 
	
		IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@EntityKey				= @ApplicationId
		,	@AuditAction			= 'Search'
		,	@SystemEntityType		= @SystemEntityType
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

	END
END
GO
	

