IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationOperationXApplicationRoleSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleSearch'
	DROP Procedure ApplicationOperationXApplicationRoleSearch
END
GO

PRINT 'Creating Procedure ApplicationOperationXApplicationRoleSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationOperationXApplicationRoleSearch
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
			EXEC ApplicationOperationXApplicationRoleSearch NULL	, NULL	, NULL
			EXEC ApplicationOperationXApplicationRoleSearch NULL	, 'K'	, NULL
			EXEC ApplicationOperationXApplicationRoleSearch 1		, 'K'	, NULL
			EXEC ApplicationOperationXApplicationRoleSearch 1		, NULL	, NULL
			EXEC ApplicationOperationXApplicationRoleSearch NULL	, NULL	, 'W'

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
CREATE PROCEDURE ApplicationOperationXApplicationRoleSearch
(
		@ApplicationOperationXApplicationRoleId		INT				= NULL	
	,	@ApplicationId								INT				= NULL	
	,	@ApplicationOperationId						INT				= NULL	
	,	@ApplicationRoleId							INT				= NULL	
	,	@AuditId									INT						
	,	@AuditDate									DATETIME		= NULL
	,	@SystemEntityType							VARCHAR(50)		= 'ApplicationOperationXApplicationRole' 
	,	@ApplicationMode	   						INT				= NULL		
	,	@AddAuditInfo								INT				= 1
	,	@AddTraceInfo								INT				= 0
	,	@ReturnAuditInfo							INT				= 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	
	IF @AddTraceInfo = 1 
	BEGIN
		-- TRACE --
		DECLARE @InputParametersLocal	VARCHAR(500)  
		DECLARE @InputValuesLocal		VARCHAR(5000)  

		SET @InputParametersLocal		= 'ApplicationOperationXApplicationRoleId' + ', ' 
		SET @InputValuesLocal			= CAST(@ApplicationOperationXApplicationRoleId AS VARCHAR(50)) + ', '

		EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.ApplicationOperationXApplicationRoleSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		-- TRACE --		
	END 
			
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	SELECT	a.ApplicationOperationXApplicationRoleId	
		,	a.ApplicationId	
		,	a.ApplicationOperationId						
		,	a.ApplicationRoleId								
		,	b.Name		AS	'ApplicationOperation'			
		,	c.Name		AS	'ApplicationRole'
	INTO #TempMain
	FROM		dbo.ApplicationOperationXApplicationRole	a
	INNER JOIN	ApplicationOperation				b	ON	a.ApplicationOperationId	=	b.ApplicationOperationId
	INNER JOIN	ApplicationRole						c	ON	a.ApplicationRoleId	=	c.ApplicationRoleId
	WHERE   a.ApplicationOperationXApplicationRoleId = ISNULL(@ApplicationOperationXApplicationRoleId, a.ApplicationOperationXApplicationRoleId )
	AND		a.ApplicationOperationId = ISNULL(@ApplicationOperationId, a.ApplicationOperationId )
	AND		a.ApplicationRoleId 	 = ISNULL(@ApplicationRoleId, a.ApplicationRoleId )
	AND	   	a.ApplicationId 		 = ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY a.ApplicationOperationXApplicationRoleId	ASC

	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ApplicationOperationXApplicationRoleId < 0
	END
	
	IF @ReturnAuditInfo = 1
	BEGIN
		-- get Audit latest record matching on key, systementitytype
		SELECT		c.EntityKey			
			,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
		INTO		#HistortyInfo
		FROM 		#TempMain a		
		INNER JOIN	CommonServices.dbo.AuditHistory c		
					ON	c.EntityKey			= a.ApplicationOperationXApplicationRoleId
					AND c.SystemEntityId	= @SystemEntityTypeId
					AND c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey			
	
		-- Get Audit Date and CreatedByPersonId for given records
		SELECT		a.ApplicationOperationXApplicationRoleId	
				,	c.AuditActionId 
				,	c.CreatedDate
				,	c.CreatedByPersonId	
				, 	c.CreatedDate						AS	'UpdatedDate'
				,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
				,	d.Name								AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo										b
					ON	b.EntityKey			= a.ApplicationOperationXApplicationRoleId
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
					ON	a.ApplicationOperationXApplicationRoleId	= b.ApplicationOperationXApplicationRoleId
		ORDER BY	a.ApplicationOperationXApplicationRoleId
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
		ORDER BY	a.ApplicationOperationXApplicationRoleId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
				@SystemEntityType		= @SystemEntityType
			,	@EntityKey				= @ApplicationOperationXApplicationRoleId
			,	@AuditAction			= 'Search'
			,	@CreatedDate			= @AuditDate
			,	@CreatedByPersonId		= @AuditId
	END

END
GO


	

