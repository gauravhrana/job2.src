IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRouteInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteInsert'
	DROP  Procedure ApplicationRouteInsert
END
GO

PRINT 'Creating Procedure ApplicationRouteInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationRouteInsert
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.ApplicationRouteInsert
(
		@ApplicationRouteId			INT				= NULL 	OUTPUT	
	,	@ApplicationId								INT			= NULL	
	,	@RouteName					VARCHAR(100)	
	,	@EntityName					VARCHAR(100)					
	,	@ProposedRoute				VARCHAR(100)	
	,	@RelativeRoute				VARCHAR(200)	
	,	@Description				VARCHAR(200)
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'ApplicationRoute'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationRouteId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationRoute 
	( 
		ApplicationRouteId	
	,	ApplicationId			
	,	RouteName		
	,	EntityName		
	,	ProposedRoute	
	,	RelativeRoute	
	,	Description	
	)
	VALUES 
	(  
		@ApplicationRouteId	
	,	@ApplicationId			
	,	@RouteName					
	,	@EntityName					
	,	@ProposedRoute				
	,	@RelativeRoute				
	,	@Description				
	)

	SELECT @ApplicationRouteId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationRouteId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 