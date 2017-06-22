IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuListOfParentMenuOnly')
BEGIN
	PRINT 'Dropping Procedure MenuListOfParentMenuOnly'
	DROP  Procedure  dbo.MenuListOfParentMenuOnly
END
GO

PRINT 'Creating Procedure MenuListOfParentMenuOnly'
GO

/******************************************************************************
**		File: 
**		Name: MenuListOfParentMenuOnly
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

CREATE Procedure dbo.MenuListOfParentMenuOnly
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
	AND		(
				SELECT  COUNT(1) 
				FROM	dbo.Menu x 
				WHERE	x.ParentMenuId = a.MenuId
			) > 0	
	AND		c.IsDefault		=	1
	ORDER BY	a.SortOrder		ASC
		,		a.MenuId			ASC



END
GO