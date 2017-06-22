IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserChildrenGet')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserChildrenGet'
	DROP  Procedure ApplicationUserChildrenGet
END
GO

PRINT 'Creating Procedure ApplicationUserChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationUserChildrenGet
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

CREATE Procedure dbo.ApplicationUserChildrenGet
(
		@ApplicationUserId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'ApplicationUser'
)
AS
BEGIN

	-- GET ApplicationUserXApplicationRole Records
	SELECT	a.ApplicationUserXApplicationRoleId	
		,	a.ApplicationId	
		,	a.ApplicationUserId						
		,	a.ApplicationRoleId								
		,	b.FirstName	AS	'ApplicationUser'			
		,	c.Name		AS	'ApplicationRole'
	FROM		dbo.ApplicationUserXApplicationRole	a
	INNER JOIN	dbo.ApplicationUser					b	ON	a.ApplicationUserId	=	b.ApplicationUserId
	INNER JOIN	dbo.ApplicationRole					c	ON	a.ApplicationRoleId	=	c.ApplicationRoleId
	WHERE	a.ApplicationUserId = @ApplicationUserId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   