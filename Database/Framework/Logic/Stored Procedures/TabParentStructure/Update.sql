IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabParentStructureUpdate')
BEGIN
	PRINT 'Dropping Procedure TabParentStructureUpdate'
	DROP  Procedure  TabParentStructureUpdate
END
GO

PRINT 'Creating Procedure TabParentStructureUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TabParentStructureUpdate
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TabParentStructureUpdate
(
		@TabParentStructureId		INT			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)		
	,	@SortOrder					INT	
	,	@IsAllTab					INT
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'TabParentStructure'
)
AS
BEGIN

	UPDATE	dbo.TabParentStructure 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder	
		,	IsAllTab			=	@IsAllTab						
	WHERE	TabParentStructureId	=	@TabParentStructureId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TabParentStructure'
		,	@EntityKey				= @TabParentStructureId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO