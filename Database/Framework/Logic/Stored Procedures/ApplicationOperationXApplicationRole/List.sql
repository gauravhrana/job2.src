IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleList')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleList'
	DROP  Procedure  dbo.ApplicationOperationXApplicationRoleList
END
GO

PRINT 'Creating Procedure ApplicationOperationXApplicationRoleList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationOperationXApplicationRoleList
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

CREATE Procedure dbo.ApplicationOperationXApplicationRoleList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationOperationXApplicationRole'
)
AS
BEGIN

	SELECT	a.ApplicationOperationXApplicationRoleId
		,	a.ApplicationId		
		,	a.ApplicationOperationId					
		,	a.ApplicationRoleId							
		,	b.Name		AS	'ApplicationOperation'			
		,	c.Name		AS	'ApplicationRole'
	FROM		dbo.ApplicationOperationXApplicationRole	a
	INNER JOIN	dbo.ApplicationOperation			b	ON	a.ApplicationOperationId	=	b.ApplicationOperationId
	INNER JOIN	dbo.ApplicationRole					c	ON	a.ApplicationRoleId			=	c.ApplicationRoleId
	ORDER BY	a.ApplicationOperationXApplicationRoleId		ASC
		,		a.ApplicationOperationId						ASC
		,		a.ApplicationRoleId								ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO