IF OBJECT_ID ('dbo.Menu') IS NOT NULL
	DROP TABLE dbo.Menu
GO

CREATE TABLE dbo.Menu
(
			MenuId				INT				NOT NULL
		,	ApplicationId		INT				NOT NULL
		,	Name				VARCHAR(50)		NOT NULL
		,	Value				VARCHAR(50)		NULL
		,	ParentMenuId		INT				NULL
		,	PrimaryDevelper		VARCHAR(50)		NOT NULL
		,	IsChecked			INT				NOT NULL
		,	IsVisible			INT				NOT NULL
		,	NavigateURL			VARCHAR(500)	NOT NULL
		,	Description			VARCHAR(50)		NOT NULL
		,	SortOrder			INT				NOT NULL
		,	ApplicationModule	VARCHAR(100)	NOT NULL
)


