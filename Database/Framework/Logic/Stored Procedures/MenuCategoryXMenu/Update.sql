IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryXMenuUpdate')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryXMenuUpdate'
	DROP  Procedure  MenuCategoryXMenuUpdate
END
GO

PRINT 'Creating Procedure MenuCategoryXMenuUpdate'
GO

/******************************************************************************
**		File: 
**		Name: MenuCategoryXMenuUpdate
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

CREATE Procedure dbo.MenuCategoryXMenuUpdate
(
		@MenuCategoryXMenuId		INT		
	,	@ApplicationId								INT
	,	@MenuId						INT					
	,	@MenuCategoryId							INT					
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'MenuCategoryXMenu'
)
AS
BEGIN 

	UPDATE	dbo.MenuCategoryXMenu 
	SET		MenuId		=	@MenuId		
		,	MenuCategoryId			=	@MenuCategoryId							
	WHERE	MenuCategoryXMenuId		=	@MenuCategoryXMenuId
	AND		ApplicationId						=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @MenuCategoryXMenuId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO