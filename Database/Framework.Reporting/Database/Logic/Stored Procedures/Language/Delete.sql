IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'LanguageDelete')
BEGIN
	PRINT 'Dropping Procedure LanguageDelete'
	DROP  Procedure LanguageDelete
END
GO

PRINT 'Creating Procedure LanguageDelete'
GO
/******************************************************************************
**		File: 
**		Name: LanguageDelete
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
CREATE Procedure dbo.LanguageDelete
(
		@LanguageId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'Language'
)
AS
BEGIN

	DELETE	 dbo.Language
	WHERE	 LanguageId = @LanguageId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Language'
		,	@EntityKey				= @LanguageId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
