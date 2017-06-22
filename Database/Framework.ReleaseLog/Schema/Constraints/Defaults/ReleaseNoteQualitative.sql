IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteQualitative_Description'
)

ALTER TABLE dbo.ReleaseNoteQualitative
	ADD CONSTRAINT DF_ReleaseNoteQualitative_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteQualitative_SortOrder'
)

ALTER TABLE dbo.ReleaseNoteQualitative
	ADD CONSTRAINT DF_ReleaseNoteQualitative_SortOrder		DEFAULT 1000		FOR SortOrder
GO
