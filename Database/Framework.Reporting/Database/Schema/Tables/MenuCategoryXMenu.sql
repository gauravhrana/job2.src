
IF OBJECT_ID ('dbo.MenuCategoryXMenu') IS NOT NULL
	DROP TABLE dbo.MenuCategoryXMenu
GO
CREATE TABLE dbo.MenuCategoryXMenu
(	MenuCategoryXMenuId	INT NOT NULL,
	ApplicationId		INT NOT NULL,
	MenuId			INT NOT NULL,
	MenuCategoryId			INT NOT NULL,	
) 

GO


