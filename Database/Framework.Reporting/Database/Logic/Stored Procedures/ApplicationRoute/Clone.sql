IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRouteClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteClone'
	DROP  Procedure ApplicationRouteClone
END
GO

PRINT 'Creating Procedure ApplicationRouteClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationRouteClone
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.ApplicationRouteClone
(
		@ApplicationRouteId		INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@RouteName						VARCHAR(100)	
	,	@EntityName						VARCHAR(100)					
	,	@ProposedRoute						VARCHAR(100)	
	,	@RelativeRoute						VARCHAR(200)	
	,	@Description				VARCHAR(200)						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationRoute'
)
AS
BEGIN

	IF @ApplicationRouteId IS NULL OR @ApplicationRouteId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationRouteId OUTPUT
	END						
	
	SELECT	@ApplicationId				=	ApplicationId
		,	@RouteName					=	RouteName
		,	@EntityName					=	EntityName	
		,	@ProposedRoute				=	ProposedRoute				
		,	@RelativeRoute				=	RelativeRoute							
		,	@Description					=	Description				
	FROM	dbo.ApplicationRoute
	WHERE   ApplicationRouteId		=	@ApplicationRouteId
	ORDER BY ApplicationRouteId

	EXEC dbo.ApplicationRouteInsert 
			@ApplicationRouteId		=	NULL
		,   @ApplicationId			=   @ApplicationId
		,   @RouteName					=   @RouteName
		,	@EntityName					=	@EntityName
		,	@ProposedRoute				=	@ProposedRoute				
		,	@RelativeRoute				=	@RelativeRoute							
		,	@Description				=	@Description
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationRoute'
		,	@EntityKey				= @ApplicationRouteId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
