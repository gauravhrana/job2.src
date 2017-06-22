IF OBJECT_ID ('dbo.MenuCategory') IS NOT NULL
	DROP TABLE dbo.MenuCategory
GO

CREATE TABLE dbo.MenuCategory 
(
		MenuCategoryId			INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (100)	NOT NULL	
   	,	SortOrder			INT				NOT NULL
);

