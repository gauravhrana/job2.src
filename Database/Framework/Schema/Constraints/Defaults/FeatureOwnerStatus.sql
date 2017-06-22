IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_FeatureOwnerStatus_Description'
)

ALTER TABLE dbo.FeatureOwnerStatus
	ADD CONSTRAINT DF_FeatureOwnerStatus_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_FeatureOwnerStatus_SortOrder'
)

ALTER TABLE dbo.FeatureOwnerStatus
	ADD CONSTRAINT DF_FeatureOwnerStatus_SortOrder		DEFAULT 1000		FOR SortOrder
GO
