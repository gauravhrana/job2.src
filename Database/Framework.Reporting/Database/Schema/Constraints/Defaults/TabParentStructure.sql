IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TabParentStructure_Description'
)

ALTER TABLE dbo.TabParentStructure
	ADD CONSTRAINT DF_TabParentStructure_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TabParentStructure_SortOrder'
)

ALTER TABLE dbo.TabParentStructure
	ADD CONSTRAINT DF_TabParentStructure_SortOrder		DEFAULT 1000		FOR SortOrder
GO
