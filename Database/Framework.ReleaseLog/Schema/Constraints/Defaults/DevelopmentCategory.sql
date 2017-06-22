IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_DevelopmentCategory_Description'
)

ALTER TABLE dbo.DevelopmentCategory
	ADD CONSTRAINT DF_DevelopmentCategory_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_DevelopmentCategory_SortOrder'
)

ALTER TABLE dbo.DevelopmentCategory
	ADD CONSTRAINT DF_DevelopmentCategory_SortOrder		DEFAULT 1000		FOR SortOrder
GO
