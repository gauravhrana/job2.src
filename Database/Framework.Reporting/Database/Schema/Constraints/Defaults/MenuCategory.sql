IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_MenuCategory_Description'
)

ALTER TABLE dbo.MenuCategory
	ADD CONSTRAINT DF_MenuCategory_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_MenuCategory_SortOrder'
)

ALTER TABLE dbo.MenuCategory
	ADD CONSTRAINT DF_MenuCategory_SortOrder		DEFAULT 1000		FOR SortOrder
GO
