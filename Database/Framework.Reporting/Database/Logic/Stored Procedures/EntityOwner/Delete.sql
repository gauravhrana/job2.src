IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EntityOwnerDelete')
BEGIN
	PRINT 'Dropping Procedure EntityOwnerDelete'
	DROP  Procedure EntityOwnerDelete
END
GO

PRINT 'Creating Procedure EntityOwnerDelete'
GO
/******************************************************************************
**		File: 
**		Name: EntityOwnerDelete
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
CREATE Procedure dbo.EntityOwnerDelete
(
		@EntityOwnerId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'EntityOwner'
)
AS
BEGIN

	DELETE	 dbo.EntityOwner
	WHERE	 EntityOwnerId = @EntityOwnerId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'EntityOwner'
		,	@EntityKey				= @EntityOwnerId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
