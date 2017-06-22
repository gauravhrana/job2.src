IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuDisplayNameList')
BEGIN
	PRINT 'Dropping Procedure MenuDisplayNameList'
	DROP  Procedure  dbo.MenuDisplayNameList
END
GO

PRINT 'Creating Procedure MenuDisplayNameList'
GO

/******************************************************************************
**		File: 
**		Name: MenuDisplayNameList
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
**     ----------					   ---------
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

CREATE Procedure dbo.MenuDisplayNameList
(
		@AuditId				INT	
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'MenuDisplayName'
)
AS
BEGIN

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
	WHERE	a.ApplicationId	=	@ApplicationId
	ORDER BY LanguageId		ASC
		,	 MenuDisplayNameId			ASC
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO