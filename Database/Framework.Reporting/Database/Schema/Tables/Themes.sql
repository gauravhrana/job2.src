CREATE TABLE dbo.Themes(
	ThemeId			int			NOT NULL,
	Name			varchar(50) NOT NULL,
	Description		varchar(50) NULL,
	SortOrder		int			NULL,
	ThemeCategoryId int			NOT NULL,
	ApplicationId	int			NOT NULL
)