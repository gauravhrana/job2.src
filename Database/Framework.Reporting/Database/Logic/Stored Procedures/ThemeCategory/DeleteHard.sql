IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeCategoryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ThemeCategoryDeleteHard'
	DROP  Procedure ThemeCategoryDeleteHard
END
GO

PRINT 'Creating Procedure ThemeCategoryDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ThemeCategoryDelete
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
CREATE Procedure dbo.ThemeCategoryDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeCategory'
)
AS
BEGIN
	IF @KeyType = 'ThemeCategoryId'
	BEGIN

		DELETE	 dbo.ThemeCategory
		WHERE	 ThemeCategoryId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
