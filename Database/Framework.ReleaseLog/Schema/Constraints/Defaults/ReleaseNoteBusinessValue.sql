IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteBusinessValue_Description'
)

ALTER TABLE dbo.ReleaseNoteBusinessValue
	ADD CONSTRAINT DF_ReleaseNoteBusinessValue_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteBusinessValue_SortOrder'
)

ALTER TABLE dbo.ReleaseNoteBusinessValue
	ADD CONSTRAINT DF_ReleaseNoteBusinessValue_SortOrder		DEFAULT 1000		FOR SortOrder
GO
