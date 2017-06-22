IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipTypeDelete')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipTypeDelete'
	DROP  Procedure SystemForeignRelationshipTypeDelete
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipTypeDelete'
GO
/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipTypeDelete
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
CREATE Procedure dbo.SystemForeignRelationshipTypeDelete
(
		@SystemForeignRelationshipTypeId		INT						
	,	@AuditId								INT						
	,	@AuditDate								DATETIME	= NULL		
	,	@SystemEntityType						VARCHAR(50)	= 'SystemForeignRelationshipType'
)
AS
BEGIN
		DELETE	 dbo.SystemForeignRelationshipType
		WHERE	 SystemForeignRelationshipTypeId = @SystemForeignRelationshipTypeId

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationshipType'
		,	@EntityKey				= @SystemForeignRelationshipTypeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
GO
