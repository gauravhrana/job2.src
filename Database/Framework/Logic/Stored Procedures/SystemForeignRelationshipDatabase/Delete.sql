IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipDatabaseDelete')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipDatabaseDelete'
	DROP  Procedure SystemForeignRelationshipDatabaseDelete
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipDatabaseDelete'
GO
/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipDatabaseDelete
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
CREATE Procedure dbo.SystemForeignRelationshipDatabaseDelete
(
		@SystemForeignRelationshipDatabaseId		INT						
	,	@AuditId									INT						
	,	@AuditDate									DATETIME	= NULL		
	,	@SystemEntityType							VARCHAR(50)	= 'SystemForeignRelationshipDatabase'
)
AS
BEGIN
		DELETE	 dbo.SystemForeignRelationshipDatabase
		WHERE	 SystemForeignRelationshipDatabaseId = @SystemForeignRelationshipDatabaseId

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationshipDatabase'
		,	@EntityKey				= @SystemForeignRelationshipDatabaseId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
GO
