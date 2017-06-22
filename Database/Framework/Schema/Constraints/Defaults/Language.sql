IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Language_Description'
)

ALTER TABLE dbo.Language
	ADD CONSTRAINT DF_Language_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Language_SortOrder'
)

ALTER TABLE dbo.Language
	ADD CONSTRAINT DF_Language_SortOrder		DEFAULT 1000		FOR SortOrder
GO
