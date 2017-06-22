IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuClone')
BEGIN
	PRINT 'Dropping Procedure MenuClone'
	DROP  Procedure MenuClone
END
GO

PRINT 'Creating Procedure MenuClone'
GO

/*********************************************************************************************
**		File: 
**		Name: MenuClone
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

CREATE Procedure dbo.MenuClone
(
		@MenuId					INT			= NULL OUTPUT
	,	@ApplicationId			INT		
	,	@Name					VARCHAR(50)	
	,	@Value					VARCHAR(50)		
	,	@ParentMenuId			INT			
	,	@PrimaryDeveloper		VARCHAR(50)
	,	@Description			VARCHAR(50)		
	,	@SortOrder				INT		
	,	@IsChecked				INT
	,	@IsVisible				INT			
	,	@NavigateURL			VARCHAR(500)						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Menu'
)
AS
BEGIN

	IF @MenuId IS NULL OR @MenuId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'Menu', @MenuId OUTPUT
	END			
	
	SELECT	@ApplicationId		=	ApplicationId
		,	@Name				=	Name
		,	@Value				=	Value					
		,	@NavigateURL		=	NavigateURL			
		,	@ParentMenuId		=	ParentMenuId
		,	@PrimaryDeveloper	=	PrimaryDeveloper			
		,	@Description		=	Description				
		,	@SortOrder			=	SortOrder	
		,	@IsVisible			=	IsVisible
		,	@IsChecked			=	IsChecked								
	FROM	dbo.Menu 
	WHERE	MenuId				= @MenuId

	EXEC dbo.MenuInsert 
			@MenuId				=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Value				=	@Value
		,	@NavigateURL		=	@NavigateURL
		,	@ParentMenuId		=	@ParentMenuId
		,	@PrimaryDeveloper	=	@PrimaryDeveloper
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@IsVisible			=	@IsVisible
		,	@IsChecked			=	@IsChecked
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
