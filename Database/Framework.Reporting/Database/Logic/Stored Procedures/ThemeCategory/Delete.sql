IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeCategoryDelete')
BEGIN
	PRINT 'Dropping Procedure ThemeCategoryDelete'
	DROP  Procedure ThemeCategoryDelete
END
GO

PRINT 'Creating Procedure ThemeCategoryDelete'
GO
/******************************************************************************
**		File: 
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
CREATE Procedure dbo.ThemeCategoryDelete
(
		@ThemeCategoryId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ThemeCategory'
)
AS
BEGIN

	DELETE	 dbo.ThemeCategory
	WHERE	 ThemeCategoryId = @ThemeCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeCategory'
		,	@EntityKey				= @ThemeCategoryId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
