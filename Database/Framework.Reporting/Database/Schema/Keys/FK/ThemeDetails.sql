ALTER TABLE dbo.ThemeDetails
	ADD CONSTRAINT FK_ThemeDetails_Theme FOREIGN KEY
	(
		ThemeId
	)
	REFERENCES dbo.Theme
	(
		ThemeId 
	)
GO

ALTER TABLE dbo.ThemeDetails
	ADD CONSTRAINT FK_ThemeDetails_ThemeKey FOREIGN KEY
	(
		ThemeKeyId
	)
	REFERENCES dbo.ThemeKey
	(
		ThemeKeyId 
	)
GO

ALTER TABLE dbo.ThemeCategory
	ADD CONSTRAINT FK_ThemeDetails_ThemeCategory FOREIGN KEY
	(
		ThemeCategoryId
	)
	REFERENCES dbo.ThemeCategory
	(
		ThemeCategoryId 
	)
GO