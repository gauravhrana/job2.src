IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRouteParameterList')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteParameterList'
	DROP  Procedure  dbo.ApplicationRouteParameterList
END
GO

PRINT 'Creating Procedure ApplicationRouteParameterList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRouteParameterList
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

CREATE Procedure dbo.ApplicationRouteParameterList
(
		@AuditId				INT	
	,	@ApplicationId			INT						
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationRouteParameter'
)
AS
BEGIN

	SELECT	a.ApplicationRouteParameterId	
		,	a.ApplicationRouteId
		,	a.ApplicationId				
		,	a.ParameterName		
		,	a.ParameterValue
		,	b.RouteName AS 'RouteName'						
	 FROM	dbo.ApplicationRouteParameter a	
	 INNER JOIN dbo.ApplicationRoute b on a.ApplicationRouteId=b.ApplicationRouteId	
	 WHERE	a.ApplicationId	=	@ApplicationId
	ORDER BY  a.ApplicationRouteParameterId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO