IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryXMenuDelete')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryXMenuDelete'
	DROP  Procedure MenuCategoryXMenuDelete
END
GO

PRINT 'Creating Procedure MenuCategoryXMenuDelete'
GO
/******************************************************************************
**		File: 
**		Name: MenuCategoryXMenuDelete
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
**     ----------							-----------
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
CREATE Procedure dbo.MenuCategoryXMenuDelete
(
		@MenuCategoryXMenuId		INT			= NULL	
	,	@MenuId						INT			= NULL	
	,	@MenuCategoryId							INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'MenuCategoryXMenu'
)
AS
BEGIN

	DELETE	dbo.MenuCategoryXMenu
	WHERE	MenuCategoryXMenuId	=	ISNULL(@MenuCategoryXMenuId,	MenuCategoryXMenuId)	
	AND		MenuId					=	ISNULL(@MenuId,			MenuId)
	AND		MenuCategoryId						=	ISNULL(@MenuCategoryId,			MenuCategoryId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuCategoryXMenuId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
