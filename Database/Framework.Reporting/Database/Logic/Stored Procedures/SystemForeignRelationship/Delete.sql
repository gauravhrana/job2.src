IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipDelete')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipDelete'
	DROP  Procedure SystemForeignRelationshipDelete
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipDelete'
GO
/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipDelete
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
CREATE Procedure dbo.SystemForeignRelationshipDelete
(
		@SystemForeignRelationshipId 			INT						
	,	@AuditId								INT						
	,	@AuditDate								DATETIME	= NULL		
	,	@SystemEntityType						VARCHAR(50)	= 'SystemForeignRelationship'
)
AS
BEGIN

	DELETE	 dbo.SystemForeignRelationship
	WHERE	 SystemForeignRelationshipId = @SystemForeignRelationshipId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationship'
		,	@EntityKey				= @SystemForeignRelationshipId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
