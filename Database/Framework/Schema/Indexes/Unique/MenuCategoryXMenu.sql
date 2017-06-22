IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[MenuCategoryXMenu]')
	AND		name	= N'UQ_MenuCategoryXMenu_ApplicationId_MenuId_MenuCategoryId'
)
BEGIN
	PRINT	'Dropping UQ_MenuCategoryXMenu_ApplicationId_MenuId_MenuCategoryId'
	ALTER	TABLE dbo.MenuCategoryXMenu
	DROP	CONSTRAINT	UQ_MenuCategoryXMenu_ApplicationId_MenuId_MenuCategoryId
END
GO

ALTER TABLE dbo.MenuCategoryXMenu
ADD CONSTRAINT UQ_MenuCategoryXMenu_ApplicationId_MenuId_MenuCategoryId UNIQUE NONCLUSTERED
(
		ApplicationId
	,	MenuId
	,	MenuCategoryId
	
)
GO
