IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseFeature_Description'
)

ALTER TABLE dbo.ReleaseFeature
	ADD CONSTRAINT DF_ReleaseFeature_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ReleaseFeature_SortOrder'
)

ALTER TABLE dbo.ReleaseFeature
	ADD CONSTRAINT DF_ReleaseFeature_SortOrder		DEFAULT 1000		FOR SortOrder
GO
