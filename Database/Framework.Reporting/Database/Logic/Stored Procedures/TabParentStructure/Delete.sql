IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabParentStructureDelete')
BEGIN
	PRINT 'Dropping Procedure TabParentStructureDelete'
	DROP  Procedure TabParentStructureDelete
END
GO

PRINT 'Creating Procedure TabParentStructureDelete'
GO
/******************************************************************************
**		File: 
**		Name: TabParentStructureDelete
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
CREATE Procedure dbo.TabParentStructureDelete
(
		@TabParentStructureId 	INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TabParentStructure'
)
AS
BEGIN	

	DELETE	 dbo.TabParentStructure
	WHERE	 TabParentStructureId = @TabParentStructureId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TabParentStructure'
		,	@EntityKey				= @TabParentStructureId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
