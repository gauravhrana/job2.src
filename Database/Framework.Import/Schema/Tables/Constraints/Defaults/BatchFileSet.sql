IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_BatchFileSet_Description'
)

ALTER TABLE dbo.BatchFileSet
	ADD CONSTRAINT DF_BatchFileSet_Description		DEFAULT '' 		FOR Description
GO