IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationModeXRunTimeFeatureSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeXRunTimeFeatureSearch'
	DROP Procedure ApplicationModeXRunTimeFeatureSearch
END
GO

PRINT 'Creating Procedure ApplicationModeXRunTimeFeatureSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationModeXRunTimeFeatureSearch
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
			EXEC ApplicationModeXRunTimeFeatureSearch NULL	, NULL	, NULL
			EXEC ApplicationModeXRunTimeFeatureSearch NULL	, 'K'	, NULL
			EXEC ApplicationModeXRunTimeFeatureSearch 1		, 'K'	, NULL
			EXEC ApplicationModeXRunTimeFeatureSearch 1		, NULL	, NULL
			EXEC ApplicationModeXRunTimeFeatureSearch NULL	, NULL	, 'W'

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
Create procedure ApplicationModeXRunTimeFeatureSearch
(
		@ApplicationModeXRunTimeFeatureId		INT				= NULL	
	,	@ApplicationId							INT
	,	@ApplicationModeId						INT				= NULL	
	,	@RunTimeFeatureId						INT				= NULL	
	,	@AuditId								INT						
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationModeXRunTimeFeature' 
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON	
	SELECT	a.ApplicationModeXRunTimeFeatureId	
		,	a.ApplicationId	
		,	a.ApplicationModeId						
		,	a.RunTimeFeatureId								
		,	b.Name		AS	'ApplicationMode'			
		,	c.Name		AS	'RunTimeFeature'
	FROM		dbo.ApplicationModeXRunTimeFeature	a
	INNER JOIN	ApplicationMode				b	ON	a.ApplicationModeId	=	b.ApplicationModeId
	INNER JOIN	RunTimeFeature				c	ON	a.RunTimeFeatureId	=	c.RunTimeFeatureId

	WHERE   a.ApplicationModeXRunTimeFeatureId	= ISNULL(@ApplicationModeXRunTimeFeatureId, a.ApplicationModeXRunTimeFeatureId )
	AND		a.ApplicationModeId					= ISNULL(@ApplicationModeId, a.ApplicationModeId )
	AND		a.RunTimeFeatureId					= ISNULL(@RunTimeFeatureId, a.RunTimeFeatureId )
	ORDER BY a.ApplicationModeXRunTimeFeatureId	ASC
	IF @AddAuditInfo = 1 
	BEGIN

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationModeXRunTimeFeatureId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
END
GO
	

