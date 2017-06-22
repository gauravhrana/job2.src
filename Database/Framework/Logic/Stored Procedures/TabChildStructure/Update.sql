IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabChildStructureUpdate')
BEGIN
	PRINT 'Dropping Procedure TabChildStructureUpdate'
	DROP  Procedure  TabChildStructureUpdate
END
GO

PRINT 'Creating Procedure TabChildStructureUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TabChildStructureUpdate
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TabChildStructureUpdate
(
		@TabChildStructureId		INT			
	,	@Name						VARCHAR(50)				
	,	@EntityName					VARCHAR(50)			
	,	@SortOrder					INT	
	,	@TabParentStructureId		INT
	,	@InnerControlPath			VARCHAR(200)
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'TabChildStructure'
)
AS
BEGIN
	UPDATE	dbo.TabChildStructure 
	SET		Name					=	@Name				
		,	EntityName				=	@EntityName				
		,	SortOrder				=	@SortOrder	
		,	TabParentStructureId	=	@TabParentStructureId
		,	InnerControlPath		=	@InnerControlPath						
	WHERE	TabChildStructureId		=	@TabChildStructureId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TabChildStructure'
		,	@EntityKey				= @TabChildStructureId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO