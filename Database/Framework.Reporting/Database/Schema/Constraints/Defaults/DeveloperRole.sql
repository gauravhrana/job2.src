IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_DeveloperRole_Description'
)

ALTER TABLE dbo.DeveloperRole
	ADD CONSTRAINT DF_DeveloperRole_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_DeveloperRole_SortOrder'
)

ALTER TABLE dbo.DeveloperRole
	ADD CONSTRAINT DF_DeveloperRole_SortOrder		DEFAULT 1000		FOR SortOrder
GO
