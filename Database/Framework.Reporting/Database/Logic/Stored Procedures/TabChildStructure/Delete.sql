IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabChildStructureDelete')
BEGIN
	PRINT 'Dropping Procedure TabChildStructureDelete'
	DROP  Procedure TabChildStructureDelete
END
GO

PRINT 'Creating Procedure TabChildStructureDelete'
GO
/******************************************************************************
**		File: 
**		Name: TabChildStructureDelete
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
**		Date:		Author:				EntityName:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.TabChildStructureDelete
(
		@TabChildStructureId 	INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TabChildStructure'
)
AS
BEGIN	

	DELETE	 dbo.TabChildStructure
	WHERE	 TabChildStructureId = @TabChildStructureId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TabChildStructure'
		,	@EntityKey				= @TabChildStructureId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
