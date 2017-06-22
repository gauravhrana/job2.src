IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuUpdateParentMenuOnly')
BEGIN
	PRINT 'Dropping Procedure MenuUpdateParentMenuOnly'
	DROP  Procedure  MenuUpdateParentMenuOnly
END
GO

PRINT 'Creating Procedure MenuUpdateParentMenuOnly'
GO

/******************************************************************************
**		File: 
**		Name: MenuUpdateParentMenuOnly
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
CREATE Procedure dbo.MenuUpdateParentMenuOnly
(
		@MenuId					INT			
	,	@ParentMenuId			INT	
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50)	= 'Menu'
)
AS
BEGIN

	SET NOCOUNT ON
	
	UPDATE	dbo.Menu 
	SET		ParentMenuId		=	@ParentMenuId			
	WHERE	MenuId				=	@MenuId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO