IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_FileType_Description'
)

ALTER TABLE dbo.FileType
	ADD CONSTRAINT DF_FileType_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_FileType_SortOrder'
)

ALTER TABLE dbo.FileType
	ADD CONSTRAINT DF_FileType_SortOrder		DEFAULT 1000		FOR SortOrder
GO
