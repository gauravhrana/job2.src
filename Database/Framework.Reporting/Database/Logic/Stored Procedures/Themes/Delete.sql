IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDelete')
BEGIN
	PRINT 'Dropping Procedure ThemeDelete'
	DROP  Procedure ThemesDelete
END
GO

PRINT 'Creating Procedure ThemeDelete'
GO
/******************************************************************************
**		File: 
**		Name: ThemeDelete
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
CREATE Procedure dbo.ThemesDelete
(
		@ThemeId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'Theme'
)
AS
BEGIN

	DELETE	 dbo.Themes
	WHERE	 ThemeId = @ThemeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Theme'
		,	@EntityKey				= @ThemeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
