IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_UserPreferenceKey_Description'
)

ALTER TABLE dbo.UserPreferenceKey
	ADD CONSTRAINT DF_UserPreferenceKey_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_UserPreferenceKey_SortOrder'
)

ALTER TABLE dbo.UserPreferenceKey
	ADD CONSTRAINT DF_UserPreferenceKey_SortOrder		DEFAULT 1000		FOR SortOrder
GO
