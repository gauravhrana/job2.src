IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ApplicationRole_Description'
)
ALTER TABLE dbo.ApplicationRole
	ADD CONSTRAINT DF_ApplicationRole_Description		DEFAULT '' 			FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ApplicationRole_SortOrder'
)
ALTER TABLE dbo.ApplicationRole
	ADD CONSTRAINT DF_ApplicationRole_Order				DEFAULT 1000		FOR SortOrder
GO
