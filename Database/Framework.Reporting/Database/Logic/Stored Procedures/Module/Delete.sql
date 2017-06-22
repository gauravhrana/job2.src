IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ModuleDelete')
BEGIN
	PRINT 'Dropping Procedure ModuleDelete'
	DROP  Procedure ModuleDelete
END
GO

PRINT 'Creating Procedure ModuleDelete'
GO
/******************************************************************************
**		File: 
**		Name: ModuleDelete
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
CREATE Procedure dbo.ModuleDelete
(
		@ModuleId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'Module'
)
AS
BEGIN

	DELETE	 dbo.Module
	WHERE	 ModuleId = @ModuleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Module'
		,	@EntityKey				= @ModuleId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
