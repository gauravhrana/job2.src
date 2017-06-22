IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuChildrenGet')
BEGIN
	PRINT 'Dropping Procedure MenuChildrenGet'
	DROP  Procedure MenuChildrenGet
END
GO

PRINT 'Creating Procedure MenuChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: MenuChildrenGet
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

CREATE Procedure dbo.MenuChildrenGet
(
		@MenuId					INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'Menu'
)
AS
BEGIN

	-- GET Menu Records
	SELECT	a.MenuId			
		,	a.ApplicationId	
		,	a.Name	
		,	a.Value		
		,	a.ParentMenuId
		,	b.Name			AS 'Parent Menu'	
		,	a.IsChecked		
		,	a.IsVisible		
		,	a.NavigateURL		
		,	a.Description		
		,	a.SortOrder	
	FROM	dbo.Menu		a
	INNER JOIN dbo.Menu		b ON a.ParentMenuId = b.MenuId
	WHERE	a.MenuId = @MenuId

	-- GET MenuDisplayName
	SELECT	a.MenuDisplayNameId			
		,	a.ApplicationId	
		,	a.MenuId		
		,	a.LanguageId			
		,	a.Value
		,	a.IsDefault	
		,	b.Name			AS 'Menu'
		,	c.Name			AS 'Language'
	FROM	dbo.MenuDisplayName		a
	INNER JOIN dbo.Menu	b 
		ON a.MenuId = b.MenuId 
	INNER JOIN dbo.Language			c
		ON a.LanguageId = c.LanguageId
	WHERE	a.MenuId = @MenuId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   