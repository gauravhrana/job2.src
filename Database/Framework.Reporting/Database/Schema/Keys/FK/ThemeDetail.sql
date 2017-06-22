ALTER TABLE dbo.ThemeDetail
	ADD CONSTRAINT FK_ThemeDetail_Theme FOREIGN KEY
	(
		ThemeId
	)
	REFERENCES dbo.Theme
	(
		ThemeId 
	)
GO

ALTER TABLE dbo.ThemeDetail
	ADD CONSTRAINT FK_ThemeDetail_ThemeKey FOREIGN KEY
	(
		ThemeKeyId
	)
	REFERENCES dbo.ThemeKey
	(
		ThemeKeyId 
	)
GO

ALTER TABLE dbo.ThemeCategory
	ADD CONSTRAINT FK_ThemeDetail_ThemeCategory FOREIGN KEY
	(
		ThemeCategoryId
	)
	REFERENCES dbo.ThemeCategory
	(
		ThemeCategoryId 
	)
GO