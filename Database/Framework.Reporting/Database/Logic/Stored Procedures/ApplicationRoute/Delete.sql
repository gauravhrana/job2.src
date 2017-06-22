IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRouteDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteDelete'
	DROP  Procedure ApplicationRouteDelete
END
GO

PRINT 'Creating Procedure ApplicationRouteDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationRouteDelete
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ApplicationRouteDelete
(
		@ApplicationRouteId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationRoute'
)
AS
BEGIN

	DELETE	 dbo.ApplicationRoute
	WHERE	 ApplicationRouteId = @ApplicationRouteId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationRoute'
		,	@EntityKey				= @ApplicationRouteId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
