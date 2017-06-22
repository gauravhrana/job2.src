IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeUpdate')
BEGIN
	PRINT 'Dropping Procedure ThemeUpdate'
	DROP  Procedure  ThemeUpdate
END
GO

PRINT 'Creating Procedure ThemeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ThemeUpdate
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

CREATE Procedure dbo.ThemeUpdate
(
		@ThemeId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'Theme'
)
AS
BEGIN
	UPDATE	dbo.Theme 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	ThemeId	=	@ThemeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Theme'
		,	@EntityKey				= @ThemeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO