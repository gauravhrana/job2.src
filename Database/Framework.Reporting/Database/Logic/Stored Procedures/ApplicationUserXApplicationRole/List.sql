IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleList')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleList'
	DROP  Procedure  dbo.ApplicationUserXApplicationRoleList
END
GO

PRINT 'Creating Procedure ApplicationUserXApplicationRoleList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserXApplicationRoleList
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
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ApplicationUserXApplicationRoleList
(
		@AuditId				INT			= NULL		
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationUserXApplicationRole'
)
AS
BEGIN

	SELECT	a.ApplicationUserXApplicationRoleId	
		,	a.ApplicationId
		,	a.ApplicationUserId					
		,	a.ApplicationRoleId							
		,	b.FirstName		AS	'ApplicationUser'			
		,	c.Name		AS	'ApplicationRole'
	FROM		dbo.ApplicationUserXApplicationRole	a
	INNER JOIN	ApplicationUser			b	ON	a.ApplicationUserId	=	b.ApplicationUserId
	INNER JOIN	ApplicationRole					c	ON	a.ApplicationRoleId			=	c.ApplicationRoleId
	ORDER BY	a.ApplicationUserXApplicationRoleId		ASC
		,		a.ApplicationUserId						ASC
		,		a.ApplicationRoleId						ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO