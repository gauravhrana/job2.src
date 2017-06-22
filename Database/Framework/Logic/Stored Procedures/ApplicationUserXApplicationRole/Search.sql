IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationUserXApplicationRoleSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleSearch'
	DROP Procedure ApplicationUserXApplicationRoleSearch
END
GO

PRINT 'Creating Procedure ApplicationUserXApplicationRoleSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserXApplicationRoleSearch
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
			EXEC ApplicationUserXApplicationRoleSearch NULL	, NULL	, NULL
			EXEC ApplicationUserXApplicationRoleSearch NULL	, 'K'	, NULL
			EXEC ApplicationUserXApplicationRoleSearch 1	, 'K'	, NULL
			EXEC ApplicationUserXApplicationRoleSearch 1	, NULL	, NULL
			EXEC ApplicationUserXApplicationRoleSearch NULL	, NULL	, 'W'

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
Create procedure ApplicationUserXApplicationRoleSearch
(
		@ApplicationUserXApplicationRoleId		INT				= NULL	
	,	@ApplicationId							INT				= NULL
	,	@ApplicationUserId						INT				= NULL	
	,	@ApplicationRoleId						INT				= NULL	
	,	@AuditId								INT				
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationUserXApplicationRole' 
	,	@ApplicationMode						INT				= NULL		
	,	@AddAuditInfo							INT			  	= 1
	,	@AddTraceInfo							INT				= 0
	,	@ReturnAuditInfo						INT				= 0	
)
AS
BEGIN

	SET  NOCOUNT ON

	IF @AddTraceInfo = 1 
	BEGIN
		-- TRACE --
		DECLARE @InputParametersLocal	VARCHAR(500)  
		DECLARE @InputValuesLocal		VARCHAR(5000)  

		SET @InputParametersLocal		= 'ApplicationUserXApplicationRoleId' + ', ' 
		SET @InputValuesLocal			= CAST(@ApplicationUserXApplicationRoleId AS VARCHAR(50)) + ', '

		EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.ApplicationUserXApplicationRoleSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		-- TRACE --		
	END 
	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	SELECT	a.ApplicationUserXApplicationRoleId	
		,	a.ApplicationId	
		,	a.ApplicationUserId						
		,	a.ApplicationRoleId								
		,	b.FirstName	AS	'ApplicationUser'			
		,	c.Name		AS	'ApplicationRole'
	INTO		#TempMain		
	FROM		ApplicationUserXApplicationRole	a
	INNER JOIN	ApplicationUser				b	ON	a.ApplicationUserId	=	b.ApplicationUserId
	INNER JOIN	ApplicationRole						c	ON	a.ApplicationRoleId	=	c.ApplicationRoleId
	WHERE	a.ApplicationUserXApplicationRoleId = ISNULL(@ApplicationUserXApplicationRoleId,a.ApplicationUserXApplicationRoleId )
	AND a.ApplicationUserId = ISNULL(@ApplicationUserId,a.ApplicationUserId )
	AND a.ApplicationRoleId = ISNULL(@ApplicationRoleId,a.ApplicationRoleId )
	ORDER BY a.ApplicationUserXApplicationRoleId	ASC
	
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ApplicationUserXApplicationRoleId < 0
	END
	
	IF @ReturnAuditInfo = 1
	BEGIN
		-- get Audit latest record matching on key, systementitytype
		SELECT		c.EntityKey			
			,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
		INTO		#HistortyInfo
		FROM 		#TempMain a		
		INNER JOIN	CommonServices.dbo.AuditHistory c		
					ON	c.EntityKey			= a.ApplicationUserXApplicationRoleId
					AND c.SystemEntityId	= @SystemEntityTypeId
					AND c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey			
	
		-- Get Audit Date and CreatedByPersonId for given records
		SELECT		a.ApplicationUserXApplicationRoleId	
				,	c.AuditActionId 
				,	c.CreatedDate
				,	c.CreatedByPersonId	
				, 	c.CreatedDate						AS	'UpdatedDate'
				,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
				,	d.Name								AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo										b
					ON	b.EntityKey			= a.ApplicationUserXApplicationRoleId
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
					ON	a.ApplicationUserXApplicationRoleId	= b.ApplicationUserXApplicationRoleId
		ORDER BY	a.ApplicationUserXApplicationRoleId		ASC
				
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
		ORDER BY	a.ApplicationUserXApplicationRoleId		ASC
				
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
				@SystemEntityType		= @SystemEntityType
			,	@EntityKey				= @ApplicationUserXApplicationRoleId
			,	@AuditAction			= 'Search'
			,	@CreatedDate			= @AuditDate
			,	@CreatedByPersonId		= @AuditId
	END

END
GO

