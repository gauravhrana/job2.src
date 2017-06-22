IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Menu_Description'
)

ALTER TABLE dbo.Menu
	ADD CONSTRAINT DF_Menu_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Menu_SortOrder'
)

ALTER TABLE dbo.Menu
	ADD CONSTRAINT DF_Menu_SortOrder		DEFAULT 1000		FOR SortOrder
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Menu_IsChecked'
)

ALTER TABLE dbo.Menu
	ADD CONSTRAINT DF_Menu_IsChecked		DEFAULT 0		FOR IsChecked
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Menu_IsVisible'
)

ALTER TABLE dbo.Menu
	ADD CONSTRAINT DF_Menu_IsVisible		DEFAULT 1		FOR IsVisible
GO
