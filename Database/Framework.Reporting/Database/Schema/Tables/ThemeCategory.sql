-- =============================================
-- Script Template
-- =============================================
CREATE TABLE dbo.ThemeCategory(
	ThemeCategoryId	int			NOT NULL,
	ApplicationId	int			NOT NULL,
	Name			varchar(50) NOT NULL,
	Description		varchar(50) NULL,
	SortOrder		int			NULL
)