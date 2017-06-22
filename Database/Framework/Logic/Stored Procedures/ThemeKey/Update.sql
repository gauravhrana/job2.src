-- =============================================
-- Script Template
-- =============================================
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeKeyUpdate')
BEGIN
	PRINT 'Dropping Procedure ThemeKeyUpdate'
	DROP  Procedure  ThemeKeyUpdate
END
GO

PRINT 'Creating Procedure ThemeKeyUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ThemeKeyUpdate
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

CREATE Procedure dbo.ThemeKeyUpdate
(
		@ThemeKeyId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ThemeKey'
)
AS
BEGIN
	UPDATE	dbo.ThemeKey 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	ThemeKeyId	=	@ThemeKeyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeKey'
		,	@EntityKey				= @ThemeKeyId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO