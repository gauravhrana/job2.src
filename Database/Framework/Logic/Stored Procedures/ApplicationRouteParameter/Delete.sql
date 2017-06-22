IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRouteParameterDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteParameterDelete'
	DROP  Procedure ApplicationRouteParameterDelete
END
GO

PRINT 'Creating Procedure ApplicationRouteParameterDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationRouteParameterDelete
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
CREATE Procedure dbo.ApplicationRouteParameterDelete
(
		@ApplicationRouteParameterId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationRouteParameter'
)
AS
BEGIN

	DELETE	 dbo.ApplicationRouteParameter
	WHERE	 ApplicationRouteParameterId = @ApplicationRouteParameterId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationRouteParameter'
		,	@EntityKey				= @ApplicationRouteParameterId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
