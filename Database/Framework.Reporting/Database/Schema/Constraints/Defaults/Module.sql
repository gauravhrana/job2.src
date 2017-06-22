IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Module_Description'
)

ALTER TABLE dbo.Module
	ADD CONSTRAINT DF_Module_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Module_SortOrder'
)

ALTER TABLE dbo.Module
	ADD CONSTRAINT DF_Module_SortOrder		DEFAULT 1000		FOR SortOrder
GO
