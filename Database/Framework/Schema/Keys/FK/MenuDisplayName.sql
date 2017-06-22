ALTER TABLE dbo.MenuDisplayName
	ADD CONSTRAINT FK_MenuDisplayName_Language FOREIGN KEY
	(
		LanguageId
	)
	REFERENCES dbo.Language
	(
		LanguageId 
	)
GO

ALTER TABLE dbo.MenuDisplayName
	ADD CONSTRAINT FK_MenuDisplayName_Menu FOREIGN KEY
	(
		MenuId
	)
	REFERENCES dbo.Menu
	(
		MenuId 
	)
GO