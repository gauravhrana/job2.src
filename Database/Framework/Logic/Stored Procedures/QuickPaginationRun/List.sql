IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'QuickPaginationRunList')
BEGIN
	PRINT 'Dropping Procedure QuickPaginationRunList'
	DROP  Procedure  dbo.QuickPaginationRunList
END
GO

PRINT 'Creating Procedure QuickPaginationRunList'
GO

/******************************************************************************
**		File: 
**		Name: QuickPaginationRunList
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
**		Date:		Author:				WhereClause:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.QuickPaginationRunList
(
		@ApplicationId			INT
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'QuickPaginationRun'
)
AS
BEGIN

	SELECT	a.QuickPaginationRunId			
		,	a.ApplicationId		
		,	a.ApplicationUserId	
		,	a.SystemEntityTypeId
		,	a.SortClause						
		,	a.WhereClause	
		,	a.ExpirationTime
		,	b.EntityName			AS	'SystemEntityType'
		,	c.ApplicationUserName	AS	'ApplicationUserName'
	FROM	dbo.QuickPaginationRun a 
	INNER JOIN	Configuration.dbo.SystemEntityType b
		ON	a.SystemEntityTypeId = b.SystemEntityTypeId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	c
		ON	a.ApplicationUserId = c.ApplicationUserId
	WHERE	a.ApplicationId	=	@ApplicationId
	ORDER BY QuickPaginationRunId			ASC
		,	 ApplicationUserId				ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO