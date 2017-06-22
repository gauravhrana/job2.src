IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryXMenuClone')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryXMenuClone'
	DROP  Procedure MenuCategoryXMenuClone
END
GO

PRINT 'Creating Procedure MenuCategoryXMenuClone'
GO

/*********************************************************************************************
**		File: 
**		Name: MenuCategoryXMenuClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.MenuCategoryXMenuClone
(
		@MenuCategoryXMenuId		INT			= NULL	
	,	@ApplicationId							INT			= NULL
	,	@MenuId						INT			= NULL	
	,	@MenuCategoryId							INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'MenuCategoryXMenu'
)
AS
BEGIN		
	
	SELECT	@ApplicationId				= ApplicationId
		,	@MenuId		= MenuId
		,	@MenuCategoryId			= MenuCategoryId				
	FROM	dbo.MenuCategoryXMenu
	WHERE	MenuCategoryXMenuId	= @MenuCategoryXMenuId
	ORDER BY MenuCategoryXMenuId

	EXEC dbo.MenuCategoryXMenuInsert 
			@MenuCategoryXMenuId		=	NULL
		,	@ApplicationId								=	@ApplicationId
		,	@MenuId						=	@MenuId
		,	@MenuCategoryId							=	@MenuCategoryId
		,	@AuditId									=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuCategoryXMenuId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
