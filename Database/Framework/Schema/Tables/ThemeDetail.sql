CREATE TABLE dbo.ThemeDetail(
	ThemeDetailId	int			NOT NULL,
	ApplicationId	int			NOT NULL,
	ThemeKeyId	    int			NOT NULL,
	Value			varchar(50) NOT NULL,
	ThemeId			int			NOT NULL,
	ThemeCategoryId int			NOT NULL
)
