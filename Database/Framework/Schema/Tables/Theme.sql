﻿CREATE TABLE dbo.Theme(
	ThemeId			int			NOT NULL,
	Name			varchar(50) NOT NULL,
	Description		varchar(50) NULL,
	SortOrder		int			NULL,	
	ApplicationId	int			NOT NULL
)