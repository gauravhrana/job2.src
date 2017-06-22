IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_BatchFileStatus_Description'
)

ALTER TABLE dbo.BatchFileStatus
	ADD CONSTRAINT DF_BatchFileStatus_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_BatchFileStatus_SortOrder'
)

ALTER TABLE dbo.BatchFileStatus
	ADD CONSTRAINT DF_BatchFileStatus_SortOrder		DEFAULT 1000		FOR SortOrder
GO
