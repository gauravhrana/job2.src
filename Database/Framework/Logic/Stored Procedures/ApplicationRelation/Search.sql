IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationRelationSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationRelationSearch'
	DROP Procedure ApplicationRelationSearch
END
GO

PRINT 'Creating Procedure ApplicationRelationSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRelationSearch
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
			EXEC ApplicationRelationSearch NULL	, NULL	, NULL
			EXEC ApplicationRelationSearch NULL	, 'K'	, NULL
			EXEC ApplicationRelationSearch 1		, 'K'	, NULL
			EXEC ApplicationRelationSearch 1		, NULL	, NULL
			EXEC ApplicationRelationSearch NULL	, NULL	, 'W'

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
Create procedure ApplicationRelationSearch
(
		@ApplicationRelationId				INT				= NULL	
	,	@PublisherApplicationId				INT				= NULL				
	,	@SubscriberApplicationId			INT				= NULL		
	,	@SystemEntityTypeId					INT				= NULL		
	,	@SubscriberApplicationRoleId		INT				= NULL		
	,	@AuditId							INT						
	,	@AuditDate							DATETIME		= NULL
	,	@SystemEntityType					VARCHAR(50)		= 'ApplicationRelation' 
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityId AS INT
	SELECT @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	SELECT	a.ApplicationRelationId		
		,	a.PublisherApplicationId	
		,	a.SubscriberApplicationId	
		,	a.SystemEntityTypeId		
		,	a.SubscriberApplicationRoleId
		,	b.Name							AS 'SubscriberApplicationRole'
		,	c.Name							AS 'PublisherApplication'
		,	d.Name							AS 'SubscriberApplication'
		,	e.EntityName					AS 'SystemEntityType'
	INTO	#TempMain
	FROM	dbo.ApplicationRelation a
	INNER JOIN dbo.SubscriberApplicationRole	b
		ON	a.SubscriberApplicationRoleId = b.SubscriberApplicationRoleId	
	INNER JOIN AuthenticationAndAuthorization.dbo.Application c
		ON	a.PublisherApplicationId = c.ApplicationId
	INNER JOIN AuthenticationAndAuthorization.dbo.Application d
		ON	a.SubscriberApplicationId = d.ApplicationId
	INNER JOIN	dbo.SystemEntityType	e
		ON	a.SystemEntityTypeId = e.SystemEntityTypeId
	WHERE	a.ApplicationRelationId			= ISNULL(@ApplicationRelationId, a.ApplicationRelationId )
	AND		a.SystemEntityTypeId			= ISNULL(@SystemEntityTypeId, a.SystemEntityTypeId )
	AND		a.PublisherApplicationId		= ISNULL(@PublisherApplicationId, a.PublisherApplicationId )
	AND		a.SubscriberApplicationId		= ISNULL(@SubscriberApplicationId, a.SubscriberApplicationId )
	AND		a.SubscriberApplicationRoleId	= ISNULL(@SubscriberApplicationRoleId, a.SubscriberApplicationRoleId )
	ORDER BY a.ApplicationRelationId	ASC			 

	IF	@ApplicationMode = 1 
		BEGIN		
			
			DELETE FROM #TempMain
			WHERE ApplicationRelationId < 0
		
		END
			
	IF @ReturnAuditInfo = 1
		BEGIN
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.ApplicationRelationId
						AND c.SystemEntityId	= @SystemEntityId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.ApplicationRelationId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.ApplicationRelationId
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
						ON	a.ApplicationRelationId	= b.ApplicationRelationId
			ORDER BY	a.ApplicationRelationId

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
			ORDER BY	a.ApplicationRelationId

		END
	
	IF @AddAuditInfo = 1 
		BEGIN

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
				@SystemEntityType		= 'ApplicationRelation'
			,	@EntityKey				= @ApplicationRelationId
			,	@AuditAction			= 'Search'
			,	@CreatedDate			= @AuditDate
			,	@CreatedByPersonId		= @AuditId	
		END

END
GO
	

