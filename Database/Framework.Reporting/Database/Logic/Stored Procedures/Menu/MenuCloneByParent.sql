IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCloneByParentMenu')
BEGIN
	PRINT 'Dropping Procedure MenuCloneByParentMenu'
	DROP  Procedure MenuCloneByParentMenu
END
GO

PRINT 'Creating Procedure MenuCloneByParentMenu'
GO

/*********************************************************************************************
**		File: 
**		Name: MenuCloneByParentMenu
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

CREATE Procedure dbo.MenuCloneByParentMenu
(
		@RootMenuId				INT			= NULL
	,	@ApplicationId			INT			= NULL
	,	@TargetApplicationId	INT			= NULL		
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Menu'
)
AS
BEGIN

	WITH CTE_MENU_REC AS
	(
		SELECT	a.MenuId
			,	a.Name		
			,	a.Value	
			,	a.IsChecked		
			,	a.IsVisible		
			,	a.NavigateURL		
			,	a.Description		
			,	a.SortOrder	
			,	a.ParentMenuId 
			,	a.PrimaryDeveloper
		FROM	dbo.Menu	a
		WHERE	a.ApplicationId				=	@ApplicationId
		AND		ISNULL(a.ParentMenuId, -1)	=	ISNULL(@RootMenuId, -1)
	
		UNION ALL
	
		SELECT	a.MenuId
			,	a.Name		
			,	a.Value	
			,	a.IsChecked		
			,	a.IsVisible		
			,	a.NavigateURL		
			,	a.Description		
			,	a.SortOrder
			,	a.ParentMenuId 
			,	a.PrimaryDeveloper
		FROM	dbo.Menu		a
		INNER JOIN	CTE_MENU_REC c 		
			ON c.MenuId = a.ParentMenuId
		WHERE		a.ApplicationId				=	@ApplicationId
	)
	SELECT	a.Name		
		,	a.Value	
		,	b.Name			AS 'ParentMenu'	 
		,	a.PrimaryDeveloper
		,	a.IsChecked		
		,	a.IsVisible		
		,	a.NavigateURL		
		,	a.Description		
		,	a.SortOrder
	INTO	#TempMenuClone
	FROM	CTE_MENU_REC	a		
	LEFT JOIN dbo.Menu		b ON a.ParentMenuId = b.MenuId

	DECLARE @Name				AS	VARCHAR(50)
	DECLARE @Value				AS	VARCHAR(50)
	DECLARE @PName				AS	VARCHAR(50)
	DECLARE	@PrimaryDeveloper	AS	VARCHAR(50)
	DECLARE @Description		AS	VARCHAR(50)		
	DECLARE @SortOrder			AS	INT		
	DECLARE @IsChecked			AS	INT
	DECLARE @IsVisible			AS	INT			
	DECLARE @NavigateURL		AS	VARCHAR(500)  

	DECLARE MenuCursor CURSOR FOR	
	SELECT	Name		
		,	Value	
		,	ParentMenu
		,	PrimaryDeveloper
		,	IsChecked		
		,	IsVisible		
		,	NavigateURL		
		,	Description		
		,	SortOrder
	FROM	#TempMenuClone

	OPEN MenuCursor

	FETCH NEXT FROM MenuCursor INTO @Name, @Value, @PName, @PrimaryDeveloper, @IsChecked, @IsVisible, @NavigateURL, @Description, @SortOrder
	WHILE @@FETCH_STATUS=0
	BEGIN

		--SELECT @Name, @Value, @PName, @IsChecked, @IsVisible, @NavigateURL, @TargetApplicationId, @Description, @SortOrder

		EXEC	dbo.MenuInsert
				@MenuId					=	NULL
			,	@ApplicationId			=	@TargetApplicationId		
			,	@Name					=	@Name	
			,	@Value					=	@Value
			,	@ParentMenuId			=	NULL	
			,	@PrimaryDeveloper		=	@PrimaryDeveloper   				
			,	@Description			=	@Description	
			,	@SortOrder				=	@SortOrder		
			,	@IsChecked				=	@IsChecked
			,	@IsVisible				=	@IsVisible
			,	@NavigateURL			=	@NavigateURL 
			,	@ParentMenuName			=	@PName
			,	@AuditId				=	@AuditId

	FETCH NEXT FROM MenuCursor INTO @Name, @Value, @PName, @PrimaryDeveloper, @IsChecked, @IsVisible, @NavigateURL, @Description, @SortOrder
	END
	CLOSE		MenuCursor
	DEALLOCATE	MenuCursor

END	
GO
