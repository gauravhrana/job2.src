IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='SuperKeyDetailSearch')
BEGIN
	PRINT 'Dropping Procedure SuperKeyDetailSearch'
	DROP Procedure SuperKeyDetailSearch
END
GO

PRINT 'Creating Procedure SuperKeyDetailSearch'
GO

/******************************************************************************
**		File: 
**		Name: SuperKeyDetailSearch
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
			EXEC SuperKeyDetailSearch NULL	, NULL	, NULL
			EXEC SuperKeyDetailSearch NULL	, 'K'	, NULL
			EXEC SuperKeyDetailSearch 1		, 'K'	, NULL
			EXEC SuperKeyDetailSearch 1		, NULL	, NULL
			EXEC SuperKeyDetailSearch NULL	, NULL	, 'W'

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
Create procedure SuperKeyDetailSearch
(
		@SuperKeyDetailId			INT				= NULL 	
	,	@ApplicationId				INT				= NULL
	,	@SuperKeyId					INT				= NULL	
	,	@SystemEntityTypeId			INT				= NULL		
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'SuperKeyDetail'
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
	Select @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntityType)	
	
	SELECT	a.SuperKeyDetailId
		,	a.ApplicationId	
		,	a.EntityKey
		,	a.SuperKeyId	
		,	b.Name					AS	'SuperKey'		
	FROM		dbo.SuperKeyDetail	a
	INNER JOIN	dbo.SuperKey		b	ON	a.SuperKeyId	=	b.SuperKeyId
	WHERE	a.SuperKeyId		= ISNULL(@SuperKeyId, a.SuperKeyId )
	AND b.SystemEntityTypeId	= ISNULL(@SystemEntityTypeId, b.SystemEntityTypeId )
	AND a.SuperKeyDetailId		= ISNULL(@SuperKeyDetailId, a.SuperKeyDetailId )
	AND a.ApplicationId		    = ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.SuperKeyDetailId	ASC		
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE SuperKeyDetailId < 0
	END
			
	IF @ReturnAuditInfo = 1
		BEGIN	 
		
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.SuperKeyDetailId
						AND c.SystemEntityId	= @SystemEntityId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.SuperKeyDetailId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.SuperKeyDetailId
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
						ON	a.SuperKeyDetailId	= b.SuperKeyDetailId
			ORDER BY	a.SuperKeyDetailId

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
				ORDER BY	a.SuperKeyDetailId

			END
	IF @AddAuditInfo = 1 
		BEGIN
	
			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'SuperKeyDetail'
				,	@EntityKey				= @SuperKeyDetailId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId	
	
		END
END
GO
	

