IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryXMenuDetails')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryXMenuDetails'
	DROP  Procedure MenuCategoryXMenuDetails
END
GO

PRINT 'Creating Procedure MenuCategoryXMenuDetails'
GO


/******************************************************************************
**		File: 
**		Name: MenuCategoryXMenuDetails
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

CREATE Procedure dbo.MenuCategoryXMenuDetails
(
		@MenuCategoryXMenuId		INT			= NULL	
	,	@ApplicationId								INT			= NULL
	,	@MenuId						INT			= NULL	
	,	@MenuCategoryId							INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'MenuCategoryXMenu'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@MenuCategoryXMenuId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.MenuCategoryXMenuId	
		,	a.ApplicationId	
		,	a.MenuId						
		,	a.MenuCategoryId								
		,	b.Name				AS	'Menu'			
		,	c.Name				AS	'MenuCategory'	
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'					
	FROM		dbo.MenuCategoryXMenu	a
	INNER JOIN	dbo.Menu			b	ON	a.MenuId	=	b.MenuId
	INNER JOIN	dbo.MenuCategory					c	ON	a.MenuCategoryId	=	c.MenuCategoryId
	WHERE	a.MenuCategoryXMenuId	=	ISNULL(@MenuCategoryXMenuId,	a.MenuCategoryXMenuId)	
	AND		a.MenuId					=	ISNULL(@MenuId,			a.MenuId)
	AND		a.MenuCategoryId							=	ISNULL(@MenuCategoryId,			a.MenuCategoryId)
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuCategoryXMenuId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   