IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'LanguageUpdate')
BEGIN
	PRINT 'Dropping Procedure LanguageUpdate'
	DROP  Procedure  LanguageUpdate
END
GO

PRINT 'Creating Procedure LanguageUpdate'
GO

/******************************************************************************
**		File: 
**		Name: LanguageUpdate
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

CREATE Procedure dbo.LanguageUpdate
(
		@LanguageId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'Language'
)
AS
BEGIN
	UPDATE	dbo.Language 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	LanguageId	=	@LanguageId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Language'
		,	@EntityKey				= @LanguageId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO