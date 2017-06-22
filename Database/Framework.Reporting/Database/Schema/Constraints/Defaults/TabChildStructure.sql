IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TabChildStructure_InnerControlPath'
)

ALTER TABLE dbo.TabChildStructure
	ADD CONSTRAINT DF_TabChildStructure_InnerControlPath		DEFAULT '' 		FOR InnerControlPath
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TabChildStructure_SortOrder'
)

ALTER TABLE dbo.TabChildStructure
	ADD CONSTRAINT DF_TabChildStructure_SortOrder		DEFAULT 1000		FOR SortOrder
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TabChildStructure_EntityName'
)

ALTER TABLE dbo.TabChildStructure
	ADD CONSTRAINT DF_TabChildStructure_EntityName		DEFAULT '' 		FOR EntityName
GO