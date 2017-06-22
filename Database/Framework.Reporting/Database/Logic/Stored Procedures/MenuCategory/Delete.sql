IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryDelete')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryDelete'
	DROP  Procedure MenuCategoryDelete
END
GO

PRINT 'Creating Procedure MenuCategoryDelete'
GO
/******************************************************************************
**		File: 
**		Name: MenuCategoryDelete
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
CREATE Procedure dbo.MenuCategoryDelete
(
		@MenuCategoryId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'MenuCategory'
)
AS
BEGIN

	DELETE	 dbo.MenuCategory
	WHERE	 MenuCategoryId = @MenuCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'MenuCategory'
		,	@EntityKey				= @MenuCategoryId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
