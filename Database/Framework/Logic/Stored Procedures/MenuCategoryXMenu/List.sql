IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryXMenuList')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryXMenuList'
	DROP  Procedure  dbo.MenuCategoryXMenuList
END
GO

PRINT 'Creating Procedure MenuCategoryXMenuList'
GO

/******************************************************************************
**		File: 
**		Name: MenuCategoryXMenuList
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

CREATE Procedure dbo.MenuCategoryXMenuList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'MenuCategoryXMenu'
)
AS
BEGIN

	SELECT	a.MenuCategoryXMenuId
		,	a.ApplicationId		
		,	a.MenuId					
		,	a.MenuCategoryId							
		,	b.Name		AS	'Menu'			
		,	c.Name		AS	'MenuCategory'
	FROM		dbo.MenuCategoryXMenu	a
	INNER JOIN	dbo.Menu			b	ON	a.MenuId	=	b.MenuId
	INNER JOIN	dbo.MenuCategory					c	ON	a.MenuCategoryId			=	c.MenuCategoryId
	ORDER BY	a.MenuCategoryXMenuId		ASC
		,		a.MenuId						ASC
		,		a.MenuCategoryId								ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO