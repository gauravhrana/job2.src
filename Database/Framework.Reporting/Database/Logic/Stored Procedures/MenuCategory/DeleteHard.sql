IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryDeleteHard'
	DROP  Procedure MenuCategoryDeleteHard
END
GO

PRINT 'Creating Procedure MenuCategoryDeleteHard'
GO
/******************************************************************************
**		Task: 
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
CREATE Procedure dbo.MenuCategoryDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'MenuCategory'
)
AS
BEGIN
	IF @KeyType = 'MenuCategoryId'
	BEGIN

		DELETE	 dbo.MenuCategory
		WHERE	 MenuCategoryId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
