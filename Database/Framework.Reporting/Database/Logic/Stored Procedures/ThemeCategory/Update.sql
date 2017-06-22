IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeCategoryUpdate')
BEGIN
	PRINT 'Dropping Procedure ThemeCategoryUpdate'
	DROP  Procedure  ThemeCategoryUpdate
END
GO

PRINT 'Creating Procedure ThemeCategoryUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ThemeCategoryUpdate
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

CREATE Procedure dbo.ThemeCategoryUpdate
(
		@ThemeCategoryId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ThemeCategory'
)
AS
BEGIN
	UPDATE	dbo.ThemeCategory 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	ThemeCategoryId	=	@ThemeCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeCategory'
		,	@EntityKey				= @ThemeCategoryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO