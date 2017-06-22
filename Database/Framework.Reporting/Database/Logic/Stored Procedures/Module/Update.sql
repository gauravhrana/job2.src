IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ModuleUpdate')
BEGIN
	PRINT 'Dropping Procedure ModuleUpdate'
	DROP  Procedure  ModuleUpdate
END
GO

PRINT 'Creating Procedure ModuleUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ModuleUpdate
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

CREATE Procedure dbo.ModuleUpdate
(
		@ModuleId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'Module'
)
AS
BEGIN
	UPDATE	dbo.Module 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	ModuleId	=	@ModuleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Module'
		,	@EntityKey				= @ModuleId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO