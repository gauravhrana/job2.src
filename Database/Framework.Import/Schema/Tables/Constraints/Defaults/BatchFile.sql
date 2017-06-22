IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_BatchFile_Description'
)

ALTER TABLE dbo.BatchFile
	ADD CONSTRAINT DF_BatchFile_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_BatchFile_Errors'
)

ALTER TABLE dbo.BatchFile
	ADD CONSTRAINT DF_BatchFile_SortOrder		DEFAULT ''		FOR Errors
GO
