IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeKeyDelete')
BEGIN
	PRINT 'Dropping Procedure ThemeKeyDelete'
	DROP  Procedure ThemeKeyDelete
END
GO

PRINT 'Creating Procedure ThemeKeyDelete'
GO
/******************************************************************************
**		File: 
**		Name: ThemeKeyDelete
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
CREATE Procedure dbo.ThemeKeyDelete
(
		@ThemeKeyId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ThemeKey'
)
AS
BEGIN

	DELETE	 dbo.ThemeKey
	WHERE	 ThemeKeyId = @ThemeKeyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeKey'
		,	@EntityKey				= @ThemeKeyId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
