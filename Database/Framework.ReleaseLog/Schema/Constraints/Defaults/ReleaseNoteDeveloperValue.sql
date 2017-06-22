IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteDeveloperValue_Description'
)

ALTER TABLE dbo.ReleaseNoteDeveloperValue
	ADD CONSTRAINT DF_ReleaseNoteDeveloperValue_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseNoteDeveloperValue_SortOrder'
)

ALTER TABLE dbo.ReleaseNoteDeveloperValue
	ADD CONSTRAINT DF_ReleaseNoteDeveloperValue_SortOrder		DEFAULT 1000		FOR SortOrder
GO
