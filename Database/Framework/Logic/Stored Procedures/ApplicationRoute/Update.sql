IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRouteUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteUpdate'
	DROP  Procedure  ApplicationRouteUpdate
END
GO

PRINT 'Creating Procedure ApplicationRouteUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRouteUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ApplicationRouteUpdate
(
		@ApplicationRouteId		INT				 
	,	@RouteName				VARCHAR(100)	
	,	@EntityName				VARCHAR(100)					
	,	@ProposedRoute			VARCHAR(100)	
	,	@RelativeRoute			VARCHAR(200)	
	,	@Description			VARCHAR(200)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityTypeId		INT
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationRoute'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationRoute 
	SET		RouteName					=	@RouteName
		,	EntityName					=	@EntityName	
		,	ProposedRoute				=	@ProposedRoute				
		,	RelativeRoute				=	@RelativeRoute							
		,	Description					=	@Description	
	WHERE	ApplicationRouteId			=	@ApplicationRouteId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationRouteId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO