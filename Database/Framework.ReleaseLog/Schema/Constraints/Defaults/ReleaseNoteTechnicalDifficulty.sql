IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteTechnicalDifficulty_Description'
)

ALTER TABLE dbo.ReleaseNoteTechnicalDifficulty
	ADD CONSTRAINT DF_ReleaseNoteTechnicalDifficulty_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteTechnicalDifficulty_SortOrder'
)

ALTER TABLE dbo.ReleaseNoteTechnicalDifficulty
	ADD CONSTRAINT DF_ReleaseNoteTechnicalDifficulty_SortOrder		DEFAULT 1000		FOR SortOrder
GO
