IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRouteList')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteList'
	DROP  Procedure  dbo.ApplicationRouteList
END
GO

PRINT 'Creating Procedure ApplicationRouteList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRouteList
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
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ApplicationRouteList
(
		@AuditId				INT	
	,	@ApplicationId			INT						
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationRoute'
)
AS
BEGIN

	SELECT	a.ApplicationRouteId
		,	a.ApplicationId			
		,	a.RouteName				
		,	a.EntityName			
		,	a.ProposedRoute				
		,	a.RelativeRoute				
		,	a.Description			
	 FROM	dbo.ApplicationRoute a	
	 WHERE	a.ApplicationId	=	@ApplicationId
	ORDER BY  a.ApplicationRouteId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO