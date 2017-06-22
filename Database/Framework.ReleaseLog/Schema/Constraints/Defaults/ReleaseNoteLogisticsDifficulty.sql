IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteLogisticsDifficulty_Description'
)

ALTER TABLE dbo.ReleaseNoteLogisticsDifficulty
	ADD CONSTRAINT DF_ReleaseNoteLogisticsDifficulty_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteLogisticsDifficulty_SortOrder'
)

ALTER TABLE dbo.ReleaseNoteLogisticsDifficulty
	ADD CONSTRAINT DF_ReleaseNoteLogisticsDifficulty_SortOrder		DEFAULT 1000		FOR SortOrder
GO
