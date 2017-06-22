IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ModuleOwnerDelete')
BEGIN
	PRINT 'Dropping Procedure ModuleOwnerDelete'
	DROP  Procedure ModuleOwnerDelete
END
GO

PRINT 'Creating Procedure ModuleOwnerDelete'
GO
/******************************************************************************
**		File: 
**		Name: ModuleOwnerDelete
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
**		Date:		Author:				Developer:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ModuleOwnerDelete
(
		@ModuleOwnerId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ModuleOwner'
)
AS
BEGIN

	DELETE	 dbo.ModuleOwner
	WHERE	 ModuleOwnerId = @ModuleOwnerId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ModuleOwner'
		,	@EntityKey				= @ModuleOwnerId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
