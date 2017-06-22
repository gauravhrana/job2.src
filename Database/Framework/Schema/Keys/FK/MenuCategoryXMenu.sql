ALTER TABLE dbo.MenuCategoryXMenu
	ADD CONSTRAINT FK_MenuCategoryXMenu_MenuCategory FOREIGN KEY
	(
		MenuCategoryId
	)
	REFERENCES MenuCategory
	(
		MenuCategoryId
	)
GO

ALTER TABLE dbo.MenuCategoryXMenu
	ADD CONSTRAINT FK_MenuCategoryXMenu_Menu FOREIGN KEY
	(
		MenuId
	)
	REFERENCES dbo.Menu
	(
		MenuId
	)
GO