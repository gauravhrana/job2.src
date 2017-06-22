IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuList')
BEGIN
	PRINT 'Dropping Procedure MenuList'
	DROP  Procedure  dbo.MenuList
END
GO

PRINT 'Creating Procedure MenuList'
GO

/******************************************************************************
**		File: 
**		Name: MenuList
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

CREATE Procedure dbo.MenuList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Menu'
)
AS
BEGIN

	SELECT	a.MenuId			
		,	a.ApplicationId	
		,	a.Name		
		,	a.Value	
		,	a.ParentMenuId
		,	b.Name			AS 'ParentMenu'	
		,	a.PrimaryDeveloper
		,	a.IsChecked		
		,	a.IsVisible		
		,	a.NavigateURL		
		,	a.Description		
		,	a.SortOrder	
		,	c.Value			AS	'MenuDisplayName'
	FROM	dbo.Menu				a
	LEFT JOIN dbo.Menu				b 
		ON a.ParentMenuId = b.MenuId
	INNER JOIN	dbo.MenuDisplayName c
		ON	a.MenuId = c.MenuId
	WHERE	a.ApplicationId	=	ISNULL(@ApplicationId, a.ApplicationId)
	AND		c.IsDefault		=	1
	ORDER BY	SortOrder		ASC
		,		MenuId			ASC



END
GO