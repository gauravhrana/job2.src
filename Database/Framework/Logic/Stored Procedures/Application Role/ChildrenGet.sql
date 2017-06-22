IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleChildrenGet')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleChildrenGet'
	DROP  Procedure ApplicationRoleChildrenGet
END
GO

PRINT 'Creating Procedure ApplicationRoleChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationRoleChildrenGet
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

CREATE Procedure dbo.ApplicationRoleChildrenGet
(
		@ApplicationRoleId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'ApplicationRole'
)
AS
BEGIN

	-- GET ApplicationOperationXApplicationRole Records
	SELECT	a.ApplicationOperationXApplicationRoleId	
		,	a.ApplicationId	
		,	a.ApplicationOperationId						
		,	a.ApplicationRoleId								
		,	b.Name		AS	'ApplicationOperation'			
		,	c.Name		AS	'ApplicationRole'
	FROM		dbo.ApplicationOperationXApplicationRole	a
	INNER JOIN	dbo.ApplicationOperation					b	ON	a.ApplicationOperationId	=	b.ApplicationOperationId
	INNER JOIN	dbo.ApplicationRole							c	ON	a.ApplicationRoleId	=	c.ApplicationRoleId
	WHERE	a.ApplicationRoleId = @ApplicationRoleId

	-- GET ApplicationOperationXApplicationRole Records
	SELECT	a.ApplicationUserXApplicationRoleId	
		,	a.ApplicationId	
		,	a.ApplicationUserId						
		,	a.ApplicationRoleId								
		,	b.FirstName	AS	'ApplicationUser'			
		,	c.Name		AS	'ApplicationRole'
	FROM		dbo.ApplicationUserXApplicationRole	a
	INNER JOIN	dbo.ApplicationUser					b	ON	a.ApplicationUserId	=	b.ApplicationUserId
	INNER JOIN	dbo.ApplicationRole					c	ON	a.ApplicationRoleId	=	c.ApplicationRoleId
	WHERE	a.ApplicationRoleId = @ApplicationRoleId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationRoleId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   