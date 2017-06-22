IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryXMenuDeleteHard')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryXMenuDeleteHard'
	DROP  Procedure MenuCategoryXMenuDeleteHard
END
GO

PRINT 'Creating Procedure MenuCategoryXMenuDeleteHard'
GO
/******************************************************************************
**		Task: 
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
CREATE Procedure dbo.MenuCategoryXMenuDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'MenuCategoryXMenu'	 
)
AS
BEGIN

	IF @KeyType = 'MenuCategoryXMenuId'
		BEGIN

			DELETE	 dbo.MenuCategoryXMenu
			WHERE	 MenuCategoryXMenuId = @KeyId

		END
	ELSE IF @KeyType = 'Menu'
		BEGIN

			DELETE	 dbo.MenuCategoryXMenu
			WHERE	 MenuId = @KeyId

		END
	ELSE IF @KeyType = 'MenuCategoryId'
		BEGIN

			DELETE	 dbo.MenuCategoryXMenu
			WHERE	 MenuCategoryId = @KeyId

		END
	ELSE IF @KeyType = 'ApplicationId'
		BEGIN

			DELETE	 dbo.MenuCategoryXMenu
			WHERE	 MenuId IN
			(
				SELECT MenuId
				FROM  dbo. dbo.Menu 
				WHERE MenuId = @KeyId	
			)
	END
	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
