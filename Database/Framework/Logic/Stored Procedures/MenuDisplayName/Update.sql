IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuDisplayNameUpdate')
BEGIN
	PRINT 'Dropping Procedure MenuDisplayNameUpdate'
	DROP  Procedure  MenuDisplayNameUpdate
END
GO

PRINT 'Creating Procedure MenuDisplayNameUpdate'
GO

/******************************************************************************
**		File: 
**		Name: MenuDisplayNameUpdate
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.MenuDisplayNameUpdate
(
		@MenuDisplayNameId		INT		
	,	@LanguageId				INT	
	,	@MenuId					INT	
	,	@Value					VARCHAR(50)		
	,	@IsDefault				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50)	= 'MenuDisplayName'
)
AS
BEGIN

	SET NOCOUNT ON
	
	UPDATE	dbo.MenuDisplayName 
	SET		Value				=	@Value					
		,	MenuId				=	@MenuId			
		,	LanguageId			=	@LanguageId
		,	IsDefault			=	@IsDefault				
	WHERE	MenuDisplayNameId	=	@MenuDisplayNameId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuDisplayNameId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO