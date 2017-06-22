IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_UserPreferenceCategory_Description'
)

ALTER TABLE dbo.UserPreferenceCategory
	ADD CONSTRAINT DF_UserPreferenceCategory_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_UserPreferenceCategory_SortOrder'
)

ALTER TABLE dbo.UserPreferenceCategory
	ADD CONSTRAINT DF_UserPreferenceCategory_SortOrder		DEFAULT 1000		FOR SortOrder
GO
