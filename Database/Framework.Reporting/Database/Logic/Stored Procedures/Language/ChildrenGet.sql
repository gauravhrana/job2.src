IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'LanguageChildrenGet')
BEGIN
	PRINT 'Dropping Procedure LanguageChildrenGet'
	DROP  Procedure LanguageChildrenGet
END
GO

PRINT 'Creating Procedure LanguageChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: LanguageChildrenGet
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

CREATE Procedure dbo.LanguageChildrenGet
(
		@LanguageId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'Language'
)
AS
BEGIN

	-- GET MenuDisplayName Records

	-- GET MenuDisplayName
	SELECT	a.MenuDisplayNameId			
		,	a.ApplicationId	
		,	a.MenuId		
		,	a.LanguageId			
		,	a.Value
		,	a.IsDefault	
		,	b.Name			AS 'Menu'
		,	c.Name			AS 'Language'
	FROM	dbo.MenuDisplayName		a
	INNER JOIN dbo.Menu	b 
		ON a.MenuId = b.MenuId 
	INNER JOIN dbo.Language			c
		ON a.LanguageId = c.LanguageId
	WHERE	a.LanguageId = @LanguageId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @LanguageId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   