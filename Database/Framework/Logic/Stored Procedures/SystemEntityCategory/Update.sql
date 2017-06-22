IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityCategoryUpdate')
BEGIN
	PRINT 'Dropping Procedure SystemEntityCategoryUpdate'
	DROP  Procedure  SystemEntityCategoryUpdate
END
GO

PRINT 'Creating Procedure SystemEntityCategoryUpdate'
GO

/******************************************************************************
**		File: 
**		Name: SystemEntityCategoryUpdate
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

CREATE Procedure dbo.SystemEntityCategoryUpdate
(
		@SystemEntityCategoryId		INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'SystemEntityCategory'
)
AS
BEGIN
	UPDATE	dbo.SystemEntityCategory 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	SystemEntityCategoryId	=	@SystemEntityCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemEntityCategory'
		,	@EntityKey				= @SystemEntityCategoryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO