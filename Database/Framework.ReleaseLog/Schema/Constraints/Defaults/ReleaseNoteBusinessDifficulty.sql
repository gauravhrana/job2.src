IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteBusinessDifficulty_Description'
)

ALTER TABLE dbo.ReleaseNoteBusinessDifficulty
	ADD CONSTRAINT DF_ReleaseNoteBusinessDifficulty_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteBusinessDifficulty_SortOrder'
)

ALTER TABLE dbo.ReleaseNoteBusinessDifficulty
	ADD CONSTRAINT DF_ReleaseNoteBusinessDifficulty_SortOrder		DEFAULT 1000		FOR SortOrder
GO
